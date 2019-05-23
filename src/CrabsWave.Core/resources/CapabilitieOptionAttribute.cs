using System;

namespace CrabsWave.Core.resources
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class CapabilitieOptionAttribute : Attribute
    {
        private readonly string value;
        private readonly bool usePropValue;

        public CapabilitieOptionAttribute(string value, bool usePropValue = false)
        {
            this.value = value;
            this.usePropValue = usePropValue;
        }

        public virtual string Value => value;
        public virtual bool UsePropValue => usePropValue;
    }
}
