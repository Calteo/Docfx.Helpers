using System;

namespace Docfx.Core
{
    [System.AttributeUsage(AttributeTargets.Property)]
    sealed class YamlHeaderAttribute : Attribute
    {
        public YamlHeaderAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
