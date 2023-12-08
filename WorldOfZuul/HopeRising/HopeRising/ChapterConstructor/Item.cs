namespace HopeRising
{
    public class Item
{
    public string Name { get; }
    public string? Description { get; }
    public string? WhereTofindItem { get; }

    public Item(string name, string? description = null ,string? whereTofindItem = null)
    {
        Name = name;
        Description = description;
        WhereTofindItem = whereTofindItem;
        
    }
}

}