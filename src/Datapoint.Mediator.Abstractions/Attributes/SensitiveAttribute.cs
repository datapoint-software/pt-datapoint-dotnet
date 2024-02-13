using System;

namespace Datapoint.Mediator.Attributes
{
    /// <summary>
    /// Marks a property as sensitive.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SensitiveAttribute : Attribute
    {
    }
}
