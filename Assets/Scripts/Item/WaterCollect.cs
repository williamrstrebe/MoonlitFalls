using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollect : MonoBehaviour
{
    public string itemName;
    public int quantity;
    
    //ontri

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WaterCollect"))
        {
            Debug.Log("Coletou água");
            collision.gameObject.transform.parent.gameObject.GetComponent<Inventory>().AddToInventory(this.itemName, this.quantity);
            //gameObject.transform.Find("Player").GetComponent<Inventory>().AddToInventory(this.itemName, this.quantity);
            //Inventory.AddToInventory(this.itemName, this.quantity);
            //collision.GetComponent<Inventory>().AddToInventory(this.itemName, this.quantity);
        }
    }
}
