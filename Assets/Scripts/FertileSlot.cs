using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertileSlot : MonoBehaviour
{

    public SpriteRenderer rendererObj;
    private int status; // 0 untilled, 1 tilled, 2 seeded, 3 harvestable, 4 ruined
    public Sprite dug;
    public Sprite dugWatered;
    //public Sprite seed;
    //public Sprite seedWatered;

    private int hitpoints;

    private void Start() {
        status = 0;
        hitpoints = 1;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("DugArea")) {

            Debug.Log("Detectou colisão da enxada");
            if (status == 0) { 
                hitpoints--;
                if (hitpoints == 0) {
                    status = 1;
                    rendererObj.sprite = dug;
                }
            }
            else if(status == 1) {
                Debug.Log("Esse slot já foi preparado");
            }
        }

    }
}
