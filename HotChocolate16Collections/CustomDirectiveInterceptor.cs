using HotChocolate.Configuration;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Configurations;
using System.ComponentModel.DataAnnotations;

namespace HotChocolate16Collections;

/// <summary>
/// Apply a directive from an attribute.
/// </summary>
class CustomDirectiveInterceptor : TypeInterceptor
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
            Apply(obj, i => i.RuntimeType.GetCustomAttributes(true), discoveryContext);

            // Apply to fields
            foreach (var field in obj.Fields)
            {
                ApplyFieldDirectives(field, f => f?.Member?.GetCustomAttributes(true), discoveryContext);

                // Apply to arguments
                foreach (var argument in field.Arguments)
                {
                    if (argument is not { Parameter: { } parameter })
                    {
                        continue;
                    }

                    ApplyFieldDirectives(argument, f => parameter.GetCustomAttributes(true), discoveryContext);
                }
            }
        }

        // Input object types
        if (configuration is InputObjectTypeConfiguration inputObjectTypeDefinition)
        {
            Apply(inputObjectTypeDefinition, i => i.RuntimeType.GetCustomAttributes(true), discoveryContext);


            foreach (var field in inputObjectTypeDefinition.Fields)
            {
                if (field.Property != null)
                {
                    ApplyFieldDirectives(field, f => f?.Property?.GetCustomAttributes(true), discoveryContext);
                }
            }
        }
    }

    void Apply<T>(T field, Func<T, object[]?> attrs, ITypeDiscoveryContext discoveryContext)
        where T : TypeConfiguration
    {
        var attributes = attrs(field);
        if (attributes == null || attributes.Length == 0)
        {
            return;
        }

        foreach (var attribute in attributes)
        {
            if (attribute is CustomAttribute)
            {
                // ... other stuff around the specific custom attribute...

                // Add to the field.
                var directive = new CustomDirective();
                var validationTypeRef = TypeReference.CreateDirective(discoveryContext.TypeInspector.GetType(directive.GetType()));

                field.Directives.Add(new DirectiveConfiguration(
                    directive,
                    validationTypeRef));
            }
        }
    }




    void ApplyFieldDirectives<T>(T field, Func<T, object[]?> attrs, ITypeDiscoveryContext discoveryContext)
        where T : FieldConfiguration
    {
        var attributes = attrs(field);
        if (attributes == null || attributes.Length == 0)
        {
            return;
        }

        foreach (var attribute in attributes)
        {
            if (attribute is CustomAttribute)
            {
                // ... other stuff around the specific custom attribute...

                // Add to the field.
                var directive = new CustomDirective();
                var validationTypeRef = TypeReference.CreateDirective(discoveryContext.TypeInspector.GetType(directive.GetType()));

                field.Directives.Add(new DirectiveConfiguration(
                    directive,
                    validationTypeRef));
            }
        }
    }
}
