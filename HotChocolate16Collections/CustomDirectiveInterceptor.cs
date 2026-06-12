using HotChocolate.Configuration;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Descriptors.Configurations;

namespace HotChocolate16Collections;


public class CustomDirectiveInterceptor : TypeInterceptor
{
    /// <summary>
    /// Apply directives from type attributes.
    /// </summary>
    /// <param name="discoveryContext"></param>
    /// <param name="definition"></param>
    public override void OnBeforeRegisterDependencies(ITypeDiscoveryContext discoveryContext, TypeSystemConfiguration definition)
    {
        if (definition is ObjectTypeConfiguration objectTypeDefinition)
        {
            // Location: OBJECT
            ApplyDefinitionAttributes(
                objectTypeDefinition,
                objectTypeDefinition.RuntimeType?.GetCustomAttributes(true),
                discoveryContext);

            foreach (var field in objectTypeDefinition.Fields)
            {
                // Location: FIELD_DEFINITION
                ApplyDefinitionAttributes(
                    field,
                    field.Member?.GetCustomAttributes(true),
                    discoveryContext);

                if (field.Arguments?.Count > 0)
                {
                    foreach (var argument in field.Arguments)
                    {
                        // Location: ARGUMENT_DEFINITION
                        ApplyDefinitionAttributes(
                            argument,
                            argument.Parameter?.GetCustomAttributes(true),
                            discoveryContext);
                    }
                }
            }
        }

        if (definition is InputObjectTypeConfiguration inputObjectTypeDefinition)
        {
            // Location: INPUT_OBJECT
            ApplyDefinitionAttributes(
                inputObjectTypeDefinition,
                inputObjectTypeDefinition.RuntimeType?.GetCustomAttributes(true),
                discoveryContext);

            foreach (var field in inputObjectTypeDefinition.Fields)
            {
                // Location: INPUT_FIELD_DEFINITION
                ApplyDefinitionAttributes(
                    field,
                    field.Property?.GetCustomAttributes(true),
                    discoveryContext);
            }
        }
    }

    static void ApplyDefinitionAttributes<T>(T field, object[]? attributes, ITypeDiscoveryContext discoveryContext)
        where T : IDirectiveConfigurationProvider
    {
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
