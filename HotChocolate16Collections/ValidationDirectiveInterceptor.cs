using HotChocolate.Configuration;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Configurations;
using System.ComponentModel.DataAnnotations;

namespace HotChocolate16Collections;

/// <summary>
/// Apply DataAnnotations validations as schema directives.
/// </summary>
class ValidationDirectiveInterceptor : TypeInterceptor
{
    /// <summary>
    /// Apply directives to schema types.
    /// </summary>
    /// <param name="discoveryContext"></param>
    /// <param name="configuration"></param>
    public override void OnBeforeRegisterDependencies(ITypeDiscoveryContext discoveryContext, TypeSystemConfiguration configuration)
    {
        // Object types
        if (configuration is ObjectTypeConfiguration obj)
        {
            // Apply to fields
            foreach (var field in obj.Fields)
            {
                ApplyAttributeDirectives(field, f => f?.Member?.GetCustomAttributes(true), discoveryContext);

                // Apply to arguments
                foreach (var argument in field.Arguments)
                {
                    if (argument is not { Parameter: { } parameter })
                    {
                        continue;
                    }

                    ApplyAttributeDirectives(argument, f => parameter.GetCustomAttributes(true), discoveryContext);
                }
            }
        }

        // Input object types
        if (configuration is InputObjectTypeConfiguration inputObjectTypeDefinition)
        {
            foreach (var field in inputObjectTypeDefinition.Fields)
            {
                if (field.Property != null)
                {
                    ApplyAttributeDirectives(field, f => f?.Property?.GetCustomAttributes(true), discoveryContext);
                }
            }
        }
    }

    void ApplyAttributeDirectives<T>(T field, Func<T, object[]?> attrs, ITypeDiscoveryContext discoveryContext)
        where T : FieldConfiguration
    {
        var attributes = attrs(field);
        if (attributes == null || attributes.Length == 0)
        {
            return;
        }

        ApplyAttributes(field, attributes, discoveryContext);
    }

    static void ApplyAttributes<T>(T field, IEnumerable<object> attributes, ITypeDiscoveryContext discoveryContext)
        where T : FieldConfiguration
    {
        foreach (var attribute in attributes)
        {
            if (attribute is ValidationAttribute)
            {
                // ... other stuff around the specific validation attribute...

                // Add to the field.
                var directive = new ValidationDirective();
                var validationTypeRef = TypeReference.CreateDirective(discoveryContext.TypeInspector.GetType(directive.GetType()));

                field.Directives.Add(new DirectiveConfiguration(
                    directive,
                    validationTypeRef));
            }
        }
    }
}
