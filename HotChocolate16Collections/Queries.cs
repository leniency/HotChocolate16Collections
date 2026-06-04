namespace HotChocolate16Collections;

[QueryType]
public static partial class Queries
{
    // Doesn't work.
    // Generated schema is:
    // hello(name: String!, input: InputObject!): String!

    // Expects:
    // ``
    // @custom
    // hello(name: String! @custom, input: InputObject! @custom): String!
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