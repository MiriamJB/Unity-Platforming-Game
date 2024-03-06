using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class animationStateController : MonoBehaviour {
    Animator animator; // holds the animator component
    private String currentDirection; // used to keep track of the player's direction
    public Player player; // used to get values from the Player script (horizontalInput, inAir, jumpKeyWasPressed)
    private float horizontalInput; // holds the horizontalInput value from player;


    void Start() {
        animator = GetComponent<Animator>(); // get the animator component
        currentDirection = "right"; // player starts out facing right
    }

    void Update() {
        horizontalInput = player.horizontalInput; // define horizontalInput by getting it from player

        // check if player is moving
        if (horizontalInput != 0) {
            animator.SetBool("isRunning", true); // if the input is not zero, the player is moving
        } else {
            animator.SetBool("isRunning", false); // if the input is zero, the player is not moving
        }

        // check if the direction of the player needs to change based on currentDirection & horizontalInput
        if (currentDirection.Equals("left") && horizontalInput > 0) {
            transform.rotation = Quaternion.Euler(0, 90, 0); // rotate to 90 degrees (facing right)
            currentDirection = "right";
        } else if (currentDirection.Equals("right") && horizontalInput < 0) {
            transform.rotation = Quaternion.Euler(0, 270, 0); // rotate to 270 degrees (facing left)
            currentDirection = "left";
        }

        // check if player is falling
        if (animator.GetBool("isFalling") != player.inAir) {
            animator.SetBool("isFalling", player.inAir); // set isFalling to be the same as inAir
        }

        // check if player is jumping
        if (animator.GetBool("isJumping") != player.jumpKeyWasPressed) {
            animator.SetBool("isJumping", player.jumpKeyWasPressed); // set isJumping to be the same as jumpKeyWasPressed
        }

        if (animator.GetBool("isDead") != player.isDead) {
            animator.SetBool("isDead", player.isDead);
        }
    }
}
