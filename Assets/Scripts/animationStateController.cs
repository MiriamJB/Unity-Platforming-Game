using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class animationStateController : MonoBehaviour {
    Animator animator; // used to 
    private float horizontalInput; //used to see if the player is moving
    private String currentDirection; // used to keep track of the player's direction
    public Player player; // used to get values from the Player script

    void Start() {
        animator = GetComponent<Animator>();
        currentDirection = "right";
    }

    void Update() {
        horizontalInput = Input.GetAxis("Horizontal"); // get the horizontile movement

        // check if player is moving
        if (horizontalInput != 0) {
            animator.SetBool("isRunning", true); // if the input is not zero, the player is moving
        }
        else {
            animator.SetBool("isRunning", false); // if the input is zero, the player is not moving
        }

        // change the direction of the player
        if (currentDirection.Equals("left") && horizontalInput > 0) {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            currentDirection = "right";
        }
        else if (currentDirection.Equals("right") && horizontalInput < 0) {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            currentDirection = "left";
        }

        // check if player is falling
        if (animator.GetBool("isFalling") != player.inAir) {
            animator.SetBool("isFalling", player.inAir);
        }

        // check if player is jumping
        if (animator.GetBool("isJumping") != player.jumpKeyWasPressed) {
            animator.SetBool("isJumping", player.jumpKeyWasPressed);
        }

    }
}
