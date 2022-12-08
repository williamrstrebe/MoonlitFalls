using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float runMultiplier;

    private Rigidbody2D rig;
    private Vector2 direction;

    private int currentSelected;


    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        currentSelected = 1;
    }



    private void Update()
    {
        // Capturar Direção de Movimento (pelo input)
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (Input.GetKeyDown(KeyCode.Alpha1)) { 
            Debug.Log("Equipando machado");
            currentSelected = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log("Equipando enxada");
            currentSelected = 2;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed *= runMultiplier; // Acelerar
        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed /= runMultiplier; // Desacelerar
        
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Clique Botao esquerdo down + "+ currentSelectEquip);
            if(currentSelected == 1)
            gameObject.transform.Find("ChopTreeCollider").GetComponent<BoxCollider2D>().enabled = true;
            else
            gameObject.transform.Find("DugAreaCollider").GetComponent<BoxCollider2D>().enabled = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.Log("Clique Botao esquerdo Up");
            if(currentSelected == 1)
            gameObject.transform.Find("ChopTreeCollider").GetComponent<BoxCollider2D>().enabled = false;
            else
            gameObject.transform.Find("DugAreaCollider").GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void FixedUpdate()
    {
        rig.MovePosition(rig.position + direction * speed * Time.fixedDeltaTime);
    }

}


/**
 * 
 *  
 * **/