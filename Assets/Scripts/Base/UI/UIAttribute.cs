public class UIAttribute : BaseAttribute
{
    public string Name { get; }

    public UIAttribute(string name)
    {
        this.Name = name;
    }
}