using AutoFixture.Kernel;

namespace DataGenerator.Generators.Interfaces;

internal interface ITypeDataGenerator
{
    bool TryGenerate(Type type, string? name, ISpecimenContext context, out object? value);
}
