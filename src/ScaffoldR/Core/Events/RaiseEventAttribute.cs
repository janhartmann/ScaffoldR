using System;

namespace ScaffoldR.Core.Events
{
    /// <summary>
    /// Marks the class that it able to raise events after execution.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RaiseEventAttribute : Attribute
    {
        /// <summary>
        /// Whether the events should be raised or not
        /// </summary>
        public bool Enabled { get; set; } = true;
    }
}