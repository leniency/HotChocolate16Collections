namespace HotChocolate16Collections;

/// <summary>
/// Directive for DataAnnotation validations. This is applied in the
/// <see cref="ValidationDirectiveInterceptor"/> from DataAnnotations.
/// </summary>
[DirectiveType(DirectiveName,
    DirectiveLocation.InputFieldDefinition
    | DirectiveLocation.ArgumentDefinition
    | DirectiveLocation.FieldDefinition)]
public class ValidationDirective
{
    public const string DirectiveName = "validation";

    // Other validation properties...
}
