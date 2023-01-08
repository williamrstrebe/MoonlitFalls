using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item
{
    public string itemName;
    public int quantity;
    public Image image;

    public Item(string name, int quantity)
    {
        this.itemName = name;
        this.quantity = quantity;
    }

}
