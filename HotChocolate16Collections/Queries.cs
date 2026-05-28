using System.ComponentModel.DataAnnotations;

namespace HotChocolate16Collections;

[QueryType]
public static partial class Queries
{
    // Doesn't work.
    // Generated schema is:
    // hello(name: String!): String!

    // Expects:
    // hello(name: String! @validation): String!

    public static string Hello([StringLength(50)] string name) => $"Hello, {name}!";

    public static NestedInstance NestedInstance() => new();
}

public class NestedInstance
{
    // Works as expected.
    public string Hello1([StringLength(50)] string name) => $"Hello, {name}!";
}

[ObjectType<NestedInstance>]
public static partial class NestedExtensions
{
    // Doesn't work.
    public static string HelloExt([StringLength(50)] string name) => $"Hello, {name}!";
}


[QueryType]
public class InstanceQuery
{
    // Works as expected.

    public string Hello2([StringLength(50)] string name) => $"Hello, {name}!";
}
