using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public string itemName;
    public int quantity;
    public ItemSlot(string name, int quantity)
    {
        this.itemName = name;
        this.quantity = quantity;
    }

    public int add(int number)
    {
        if (quantity + number > 999)
        {
            number = this.quantity + number - 999;
            this.quantity = 999;
        }
        else
        {
            this.quantity += number;
            number = 0;
        }

        // se return for > 0, o método que chamou deve acomodar os demais itens em outro slot
        return number;

    }
}
