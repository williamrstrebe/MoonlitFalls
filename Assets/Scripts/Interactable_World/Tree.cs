using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour { 

    private int hitpoints = 3;
    [SerializeField] private ParticleSystem particles;
    private bool available = false; // setar pra false quando corrigir
    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("Player") && hitpoints > 0) {
            //   Hit();
            Debug.Log("Player entrou do Range de uma árvore");
            available = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player") && hitpoints > 0)
        {
            Debug.Log("Player saiu do Range de uma árvore");
            available = false;
        }
    }

    public int Hit() {

        int hit = 0;

        if (available)
        {
            hit = 1;
            Debug.Log("Detectou colisão");
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
