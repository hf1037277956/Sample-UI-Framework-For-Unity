using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class UIAttribute : Attribute
{
    public string Name { get; }

    public UIAttribute(string name)
    {
        this.Name = name;
    }
}