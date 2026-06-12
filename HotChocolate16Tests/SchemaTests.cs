using HotChocolate.Execution;
using HotChocolate16Collections;
using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate16Tests;

[TestClass]
public sealed class SchemaTests
{
    [TestMethod]
    public async Task Field_and_argument_directivesAsync()
    {
        var schema = await new ServiceCollection()
            .AddGraphQL()
            .AddHotChocolate16CollectionsTypes()
            .TryAddTypeInterceptor<CustomDirectiveInterceptor>()
            .ModifyOptions(options =>
            {
                options.DisableInternalDirectives = true;
            })
            .BuildSchemaAsync();

        var field = schema.QueryType.Fields.First(f => f.Name == "hello");

        Assert.AreEqual(
            "hello(name: String! @custom, input: InputObject! @custom): String! @custom",
            field.ToString()
        );
    }
}
