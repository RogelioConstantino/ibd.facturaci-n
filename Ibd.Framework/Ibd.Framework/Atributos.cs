using System;

namespace Ibd.Framework
{
    /// <summary>
    /// Indica que el valor de un elemento nunca podar ser <c>null</c>
    /// </summary>
    [AttributeUsage(
      AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate |
      AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class NotNullAttribute : Attribute
    {
    }
}
