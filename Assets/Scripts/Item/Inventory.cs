using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<Item> slots;
    private int maxSlots = 0;

    private void Start()
    {

    }
    private void Update()
    {

    }

    public bool AddToInventory(string name, int quantity)
    {
        if (slots == null)
        {
            //Debug.Log("instanciou");
            slots = new List<Item>(); // não está aparecendo no inspector !?!
        }
        
        if (slots.Count >= maxSlots)
        {
            Debug.Log("Inventáriio cheio");
            return false;
        }



        int i = 0;
        foreach (Item item in slots)
        {
            Debug.Log("Posicao " + (i++) + " com item " + item.itemName + ", " + item.quantity);
        }
        foreach (Item item in slots)
        {
            if (item.itemName == name)
            {
                item.quantity += quantity;
                return true;
            }
        }

        Debug.Log("chegou no inv");
        slots.Add(new Item(name, quantity));
        Debug.Log("slots agora tem " + slots.Count + "item (s)!");
        return true;
    }


    public void RemoveFromInventory(int index)
    {
        slots.RemoveAt(index);
        // Validar real necessidade de reorganizar os itens
        //for (int i = ++index; i <= inventory.Count; i++)
        //{

        //    inventory[i - 1] = inventory[i];

        //    if ((i + 1) == inventory.Count) {
        //        inventory.RemoveAt(i + 1);
        //        break;
        //    }

        //}
    }
}
