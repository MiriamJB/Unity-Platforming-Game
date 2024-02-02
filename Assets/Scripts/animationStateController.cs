using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour {
    Animator animator;
    private float horizontalInput; //used to see if the player is moving

    void Start() {
        animator = GetComponent<Animator>();
    }

    void Update() {
        horizontalInput = Input.GetAxis("Horizontal"); // get the horizontile movement

        if (horizontalInput != 0) {
            animator.SetBool("isRunning", true); // if the input is not zero, the player is moving
        } else {
            animator.SetBool("isRunning", false); // if the input is zero, the player is not moving
        }
    }
}
