using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour { 

    private int hitpoints = 3;
    [SerializeField] private ParticleSystem particles;

    public void OnHit() {
        Debug.Log("que isso");

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("ChoppingArea") && hitpoints > 0) {
            Debug.Log("Detectou colisão");
            particles.Play();
            hitpoints--;

            if (hitpoints <= 0) { 
                Debug.Log("Arvore destruida");
                Destroy(gameObject);
            }
        }
    }
}
