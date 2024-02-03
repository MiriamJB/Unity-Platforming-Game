using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class animationStateController : MonoBehaviour {
    Animator animator;
    private float horizontalInput; //used to see if the player is moving
    private String currentDirection;

    void Start() {
        animator = GetComponent<Animator>();
        currentDirection = "right";
    }

    void Update() {
        horizontalInput = Input.GetAxis("Horizontal"); // get the horizontile movement

        if (horizontalInput != 0) {
            animator.SetBool("isRunning", true); // if the input is not zero, the player is moving
        }
        else {
            animator.SetBool("isRunning", false); // if the input is zero, the player is not moving
        }

        if (currentDirection.Equals("left") && horizontalInput > 0) {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            currentDirection = "right";
        }
        else if (currentDirection.Equals("right") && horizontalInput < 0) {
            transform.rotation = Quaternion.Euler(0, 270, 0);
            currentDirection = "left";
        }
    }
}
