using System;
using System.ComponentModel.DataAnnotations;

public class Item
{
    public Guid Id { get; set; }
    [Range(0, 99)]
    public int Level { get; set; }
    [Range(0, 2)]
    public ItemType Type { get; set; }
    public DateTime CreationTime { get; set; }
}

public enum ItemType
{
    SWORD, POTION, SHIELD
};