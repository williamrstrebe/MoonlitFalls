using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public string itemName;
    public int quantity;

    public Item(string name, int quantity)
    {
        this.itemName = name;
        this.quantity = quantity;
    }

}
