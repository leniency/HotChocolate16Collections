using System.ComponentModel.DataAnnotations;

namespace HotChocolate16Collections;

[QueryType]
public static partial class Queries
{
    // Doesn't work.
    // Generated schema is:
    // hello(name: String!, input: InputObject!): String!

    // Expects:
    // ``
    // hello(name: String! @custom, input: InputObject! @custom): String! @custom
    // ``

    [Custom]
    public static string Hello([Custom] string name, [Custom] InputObject input) => $"Hello, {name}!";
}


[Custom]
public class InputObject
{
    [Custom]
    public string Name { get; set; } = default!;
}