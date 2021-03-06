﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{

    public float moveSpeed;
    
    PlayerInput playerInput;
    CharacterMovement characterMovement;
    public bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        
        playerInput = GetComponent<PlayerInput>();
        characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        if (playerInput.movement.x != 0)
        {
            if (playerInput.movement.x > 0.01f && !facingRight)
                Flip();
            else if (playerInput.movement.x < 0.01f && facingRight)
                Flip();
        }
            
        
    }


    void FixedUpdate()
    {

        characterMovement.Move(playerInput.movement, moveSpeed);
        
    }

    void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}
