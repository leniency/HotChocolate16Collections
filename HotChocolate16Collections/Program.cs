
var builder = WebApplication.CreateBuilder(args);

var g = builder.Services
    .AddGraphQLServer()
    .AddHotChocolate16CollectionsTypes()
    .ModifyPagingOptions(o =>
    {
        // Use the type name rather than the property
        // name for paging types.
        o.InferCollectionSegmentNameFromField = false;
        o.InferConnectionNameFromField = false;
    });


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGraphQL();

app.Run();
