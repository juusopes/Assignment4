using System;
using System.ComponentModel.DataAnnotations;

public class Item
{
    public Guid Id { get; set; }
    public int Level { get; set; }
    [Range(1, 99)]
    public ItemType Type { get; set; }
    public DateTime CreationTime { get; set; }
}

public enum ItemType
{
    SWORD, POTION, SHIELD
};