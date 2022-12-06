using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public string itemName;
    public int quantity;
    public ItemDrop(string name, int quantity)
    {
        this.itemName = name;
        this.quantity = quantity;
    }
    //ontri

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) {
        Debug.Log("Entrou no trigger");
            collision.GetComponent<Inventory>().AddToInventory(this.itemName, this.quantity);
            Destroy(gameObject);
        }
    }
}
