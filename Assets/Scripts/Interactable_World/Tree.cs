using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour { 

    private int hitpoints = 3;
    [SerializeField] private ParticleSystem particles;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.CompareTag("ChoppingArea") && hitpoints > 0) {
            Hit();
        }
    }

    public void Hit() {
        Debug.Log("Detectou colis�o");
        particles.Play();
        hitpoints--;

        if (hitpoints <= 0)
        {
            Debug.Log("Arvore destruida");
            Destroy(gameObject);
        }
    }
}
