using System;
using System.ComponentModel.DataAnnotations;

public class NewItem
{
    [StringLength(5)]
    public string Name { get; set; }
}