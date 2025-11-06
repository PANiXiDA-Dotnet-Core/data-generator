using System.Reflection;

using AutoFixture;
using AutoFixture.Kernel;

namespace DataGenerator.Core;

internal static class DataMutator
{
    public static T Mutate<T>(
        T source,
        IFixture fixture,
        int minChanges = 1,
        int maxChanges = 3,
        IEnumerable<string>? excludeProperties = null)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(fixture);

        var type = typeof(T);
        var properties = GetWritableInstanceProperties(type);

        var changeable = FilterChangeable(properties, excludeProperties);
        if (changeable.Length == 0)
        {
            return source;
        }

        var target = CloneWithCopy(source, type, properties);

        var random = fixture.Create<Random>() ?? new Random();
        var changesCount = BoundChangesCount(minChanges, maxChanges, changeable.Length);
        var specimen = new SpecimenContext(fixture);

        foreach (var property in PickRandomSubset(changeable, changesCount, random))
        {
            MutatePropertyValue(target, property, specimen);
        }

        AlwaysMutateProperties(target, specimen, properties);

        return target;
    }

    private static PropertyInfo[] GetWritableInstanceProperties(Type type)
    {
        return [.. type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(propertyInfo => propertyInfo.CanRead && propertyInfo.CanWrite && propertyInfo.GetIndexParameters().Length == 0)];
    }

    private static PropertyInfo[] FilterChangeable(PropertyInfo[] properties, IEnumerable<string>? excludeProperties)
    {
        var ignoredPropertyNames = typeof(MutationProperties.IMutationIgnoreProperties<>)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(property => property.Name);

        var excludes = new HashSet<string>(ignoredPropertyNames, StringComparer.OrdinalIgnoreCase);

        if (excludeProperties is not null)
        {
            excludes.UnionWith(excludeProperties);
        }

        return [.. properties.Where(property => !excludes.Contains(property.Name))];
    }

    private static T CloneWithCopy<T>(T source, Type type, PropertyInfo[] properties) where T : class
    {
        if (source is ICloneable cloneable)
        {
            return (T)cloneable.Clone();
        }

        var defaultConstructor = type.GetConstructor(Type.EmptyTypes);
        if (defaultConstructor is not null)
        {
            var target = (T)defaultConstructor.Invoke(null);
            CopyProperties(source, target, properties);
            return target;
        }
        var cloneMethod = type.GetMethod("MemberwiseClone", BindingFlags.Instance | BindingFlags.NonPublic)!;

        return (T)cloneMethod.Invoke(source, null)!;
    }

    private static void CopyProperties(object from, object to, PropertyInfo[] properties)
    {
        foreach (var property in properties)
        {
            property.SetValue(to, property.GetValue(from));
        }
    }

    private static int BoundChangesCount(int minChanges, int maxChanges, int maxPossible)
    {
        var min = Math.Max(minChanges, 1);
        var max = Math.Min(Math.Max(min, maxChanges), maxPossible);
        return max;
    }

    private static IEnumerable<PropertyInfo> PickRandomSubset(PropertyInfo[] properties, int count, Random random)
    {
        return properties.OrderBy(_ => random.Next()).Take(count);
    }

    private static void AlwaysMutateProperties(object target, SpecimenContext specimen, PropertyInfo[] properties)
    {
        var alwaysMutate = typeof(MutationProperties.IMutationForceProperties)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Select(property => property.Name);

        foreach (var name in alwaysMutate)
        {
            var property = properties.FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.OrdinalIgnoreCase));

            if (property is not null && property.CanWrite)
            {
                MutatePropertyValue(target, property, specimen);
            }
        }
    }

    private static void MutatePropertyValue(object target, PropertyInfo property, SpecimenContext specimen)
    {
        var targetType = property.PropertyType;
        var underlying = Nullable.GetUnderlyingType(targetType);
        var realType = underlying ?? targetType;

        const int tries = 5;
        var oldValue = property.GetValue(target);
        object? newValue = null;

        for (int i = 0; i < tries; i++)
        {
            var candidate = ResolveSpecimen(specimen, property, realType);
            newValue = candidate;

            if (!AreEqualSimple(oldValue, newValue))
            {
                break;
            }
        }

        property.SetValue(target, newValue);
    }

    private static object? ResolveSpecimen(SpecimenContext specimen, PropertyInfo property, Type realType)
    {
        var candidate = specimen.Resolve(property);
        if (candidate is NoSpecimen)
        {
            candidate = specimen.Resolve(realType);
        }

        return candidate is NoSpecimen ? null : candidate;
    }

    private static bool AreEqualSimple(object? a, object? b)
    {
        if (a is null && b is null)
        {
            return true;
        }
        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }
}
