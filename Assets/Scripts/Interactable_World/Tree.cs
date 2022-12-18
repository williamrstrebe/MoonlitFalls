using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour { 

    private int hitpoints = 3;
    [SerializeField] private ParticleSystem particles;
    private bool available = true; // setar pra false quando corrigir
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("ChoppingArea") && hitpoints > 0) {
            //   Hit();
            available = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("ChoppingArea") && hitpoints > 0)
        {
            available = false;
        }
    }

    public int Hit() {

        int hit = 0;

        if (available)
        {
            hit = 1;
            Debug.Log("Detectou colis�o");
            particles.Play();
            hitpoints--;

            if (hitpoints <= 0)
            {
                Debug.Log("Arvore destruida");
                Destroy(gameObject);
            }
        }
        else 
        {
            hit = -1;
            Debug.Log("Detectou clique, mas fora do alcance");
            
        }

        return hit;
    }
}
