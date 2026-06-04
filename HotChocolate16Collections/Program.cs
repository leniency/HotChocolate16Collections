using HotChocolate16Collections;

var builder = WebApplication.CreateBuilder(args);



var graphBuilder = builder.Services
    .AddGraphQLServer()
    .ExportSchemaOnStartup("./schema.graphql")
    .AddAuthorization()
    .AddHotChocolate16CollectionsTypes()
    .TryAddTypeInterceptor<CustomDirectiveInterceptor>()
    .ModifyOptions(options =>
    {
        options.DisableInternalDirectives = true;
    });


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGraphQL();

app.Run();


namespace HotChocolate16Collections
{
    public partial class Program { }
}
