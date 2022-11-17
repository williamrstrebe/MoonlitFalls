using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float runMultiplier;

    private Rigidbody2D rig;
    private Vector2 direction;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (Input.GetKeyDown(KeyCode.LeftShift))
            speed *= runMultiplier;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed /= runMultiplier;
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