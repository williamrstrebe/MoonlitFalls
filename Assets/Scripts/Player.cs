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
    private string selectedEquipCollider;

    private bool lockForAnim;

    private void Start()
    {
        lockForAnim = false;
        rig = GetComponent<Rigidbody2D>();
        currentSelected = 1;
        selectedEquipCollider = "ChopTreeCollider";
    }



    private void Update()
    {
        // Capturar Dire��o de Movimento (pelo input)
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        
        if (Input.GetKeyDown(KeyCode.Alpha1)) { 
            Debug.Log("Equipando machado");
            currentSelected = 1;
            selectedEquipCollider = "ChopTreeCollider";
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            Debug.Log("Equipando enxada");
            currentSelected = 2;
            selectedEquipCollider = "DugAreaCollider";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Equipando regador");
            currentSelected = 3;
            selectedEquipCollider = "WaterCollectCollider";
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed *= runMultiplier; // Acelerar
        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed /= runMultiplier; // Desacelerar

        if (Input.GetMouseButtonDown(0))
        {

            if (!lockForAnim)
                MouseAction();
            else
                Debug.Log("Locked for Animation");
            


            //gameObject.transform.Find(selectedEquipCollider).gameObject.GetComponent<BoxCollider2D>().enabled = true;
            //Debug.Log("Clique Botao esquerdo down + "+ currentSelectEquip);
            //if (currentSelected == 1)
            //gameObject.transform.Find("ChopTreeCollider").GetComponent<BoxCollider2D>().enabled = true;
            //else if (currentSelected == 2)
            //    gameObject.transform.Find("DugAreaCollider").GetComponent<BoxCollider2D>().enabled = true;
            //else if (currentSelected == 3)
            //    gameObject.transform.Find("WaterCollectCollider").GetComponent<BoxCollider2D>().enabled = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            gameObject.transform.Find(selectedEquipCollider).gameObject.GetComponent<BoxCollider2D>().enabled = false;
            //Debug.Log("Clique Botao esquerdo Up");
            //if(currentSelected == 1)
            //    gameObject.transform.Find("ChopTreeCollider").GetComponent<BoxCollider2D>().enabled = false;
            //else if (currentSelected == 2)
            //    gameObject.transform.Find("DugAreaCollider").GetComponent<BoxCollider2D>().enabled = false;
            //else if (currentSelected == 3)
            //    gameObject.transform.Find("WaterCollectCollider").GetComponent<BoxCollider2D>().enabled = false;
        }

        if (currentSelected == 1) { 
        // só pra tirar o warning
        }
    }

    private void MouseAction() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            Debug.Log("CLICKED " + hit.collider.name);

            if (hit.collider.name == "Tree")
                if (currentSelected == 1) {
                    if (hit.transform.GetComponent<Tree>().Hit() == 1) { //se colidiu e estava no alcance, retorna 1
                        // descomentar quando tiver animação
                        // controla para nao dar varios cliques seguidos
                        //this.lockForAnim = true; // deve ser setado pra false no ultimo frame do Animator
                    }
                }
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