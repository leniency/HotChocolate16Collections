

using HotChocolate.Configuration;
using HotChocolate.Types.Descriptors.Configurations;

namespace HotChocolate16Collections;

/// <summary>
/// Base type interceptor for custom directives. 
/// </summary>
abstract class CustomDirectiveInterceptorBase : TypeInterceptor
{

    /// <summary>
    /// Apply directives to a field.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="field"></param>
    /// <param name="attrs"></param>
    /// <param name="discoveryContext"></param>
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

    protected abstract void ApplyAttributes<T>(T field, IEnumerable<object> attributes, ITypeDiscoveryContext discoveryContext)
        where T : FieldConfiguration;
}
