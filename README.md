# PANiXiDA.DataGenerator

**PANiXiDA.DataGenerator** is a library for generating realistic test and sample data based on **Bogus** and **AutoFixture**.  
It can automatically populate models (including nested objects and collections), control how data is mutated, and produce consistent results using scoped deterministic seeds.

The library can be used in:
- integration tests
- load testing
- prototyping
- demos and mock environments
- any scenario requiring meaningful sample data

---

## Installation

```sh
dotnet add package PANiXiDA.DataGenerator
````

---

## Quick Start

```csharp
var data = new DataFacade(locale: "en", seed: 123);

var user = data.Create<User>();   // fully generated model
var modifiedUser = data.Mutate(user);   // original object with several values changed
```

---

## Features

### • Automatic model population with **realistic values**

The generator provides meaningful data for common model fields (e.g. Emails, Names, Dates, Addresses).

### • Full support for nested models and collections

`List<T>`, `T[]`, `Dictionary<TKey, TValue>`, inner class models — are all populated recursively.

### • Nullable fields are handled naturally

Nullable properties (`string?`, `int?`, `DateTime?`, etc.) are left `null` with a small probability.

### • Deterministic output

Providing the same `seed` (optionally combined with `scope`) produces the same result across runs.

---

## Supported Types and Property Rules

### Strings (`string`)

| Field Name Contains                  | Example Output             |
| ------------------------------------ | -------------------------- |
| `email`                              | realistic e-mail address   |
| `phone`, `mobile`                    | phone number               |
| `fullname`, `name`, `fio`            | full personal name         |
| `firstname`                          | given name                 |
| `lastname`, `surname`                | family name                |
| `username`, `login`                  | username                   |
| `password`, `pwd`                    | password containing digits |
| `url`, `link`                        | website URL                |
| `avatar`                             | avatar image URL           |
| `country`                            | country name               |
| `city`                               | city                       |
| `street`                             | address / street           |
| `postcode`, `postal`, `zip`          | postal code                |
| `title`, `subject`                   | short text                 |
| `desc`, `message`, `text`, `content` | sentence or paragraph      |
| `company`                            | company name               |
| `iban`, `account`                    | payment account number     |
| `currency`                           | currency code              |
| otherwise                            | alphanumeric string        |

### Numbers (`int`, `long`, `decimal`, `float`, `double`, etc.)

| Field Name Contains             | Rule                                        |
| ------------------------------- | ------------------------------------------- |
| `id` or ends with `id`          | generates small or zero-like identity value |
| `age`                           | age in realistic range (18–70)              |
| `count`, `qty`, ends with `num` | quantity-like numeric value                 |
| `price`, `amount`, `sum`        | monetary values                             |
| `year`                          | realistic year value                        |
| otherwise                       | bounded random number                       |

### Boolean (`bool`)

* Fields like `IsActive`, `IsEnabled`, `HasAccess` → mostly `true`
* Fields like `IsDeleted`, `IsBlocked`, `IsDisabled` → mostly `false`

### Date and Time

| Type                               | Rule                  |
| ---------------------------------- | --------------------- |
| `CreatedAt`                        | past date             |
| `UpdatedAt`, `ModifiedAt`          | recent date           |
| `DeletedAt`                        | recent date or null   |
| `BirthDate`, `Birthday`            | realistic birthday    |
| `Registered`, `Signup`             | past date             |
| `DateOnly`, `TimeOnly`, `TimeSpan` | generated accordingly |

### Collections

| Type                                    | Behavior                               |
| --------------------------------------- | -------------------------------------- |
| `T[]`                                   | array of generated elements (size 2–6) |
| `List<T>`, `IList<T>`, `IEnumerable<T>` | list of generated elements (size 2–6)  |
| `Dictionary<TKey, TValue>`              | unique keys, generated values          |

### Other types

| Type   | Behavior                          |
| ------ | --------------------------------- |
| `Guid` | random GUID                       |
| `char` | letter / digit / printable symbol |
| `enum` | random non-default value          |
| `Uri`  | valid generated URI               |

---

## Mutation

```csharp
var mutated = data.Mutate(user, minChanges: 1, maxChanges: 3);
```

Mutation will:

* clone the object
* randomly change several writable properties
* preserve nullable behavior
* work recursively for nested objects

### Fields excluded from mutation (never changed)

By default, the following fields are **never mutated**:

```
Id
CreatedAt
UpdatedAt
DeletedAt
```

### Fields always mutated

The following fields are **always mutated**, even if no others change:

```
Login
Password
```

You can also explicitly exclude properties:

```csharp
var mutated = data.Mutate(user, excludeProperties: new[] { nameof(User.Email) });
```

---

## Deterministic Output and Scopes

```csharp
var a = new DataFacade(seed: 42);
var b = new DataFacade(seed: 42);

a.Create<User>().Should().BeEquivalentTo(b.Create<User>()); // true
```

Scopes allow independent data streams:

```csharp
var authData = new DataFacade(scope: "auth");
var orderData = new DataFacade(scope: "orders");
```

---

## License

Apache 2.0 — free for commercial and open use.

---

## Summary

| Feature                               | Supported |
| ------------------------------------- | :-------: |
| Realistic data generation             |     ✅     |
| Nested object population              |     ✅     |
| Collections & dictionaries            |     ✅     |
| Nullable handling                     |     ✅     |
| Deterministic output via seed & scope |     ✅     |
| Mutation with field rules             |     ✅     |
| Usable outside tests                  |     ✅     |

---