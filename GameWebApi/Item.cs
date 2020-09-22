using System;

public class Item
{
    public string Name { get; set; }
    public int Level { get; set; }
    public ItemType Type { get; set; }
    public DateTime CreationTime { get; set; }
}

public enum ItemType
{
    SWORD, POTION, SHIELD
};