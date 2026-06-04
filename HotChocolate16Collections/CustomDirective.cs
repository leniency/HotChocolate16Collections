namespace HotChocolate16Collections;

/// <summary>
/// A custom directive.
/// </summary>
[DirectiveType(DirectiveName,
    DirectiveLocation.InputFieldDefinition
    | DirectiveLocation.Object
    | DirectiveLocation.InputObject
    | DirectiveLocation.ArgumentDefinition
    | DirectiveLocation.FieldDefinition)]
public class CustomDirective
{
    public const string DirectiveName = "custom";
}
