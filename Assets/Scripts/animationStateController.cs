using UnityEngine;

public class animationStateController : MonoBehaviour {
    Animator animator; // holds the animator component
    public Player player; // used to get values from the Player script (horizontalInput, inAir, jumpKeyWasPressed)
    private float horizontalInput; // holds the horizontalInput value from player;


    void Start() {
        animator = GetComponent<Animator>(); // get the animator component
    }

    void Update() {
        horizontalInput = player.horizontalInput; // define horizontalInput by getting it from player

        // check if player is moving
        if (horizontalInput != 0) {
            animator.SetBool("isRunning", true); // if the input is not zero, the player is moving
        } else {
            animator.SetBool("isRunning", false); // if the input is zero, the player is not moving
        }

        // check if the direction of the player needs to change based on isFacingRight bool from player
        if (player.isFacingRight) {
            transform.rotation = Quaternion.Euler(0, 90, 0); // rotate to 90 degrees (facing right)
        } else if (!player.isFacingRight) {
            transform.rotation = Quaternion.Euler(0, 270, 0); // rotate to 270 degrees (facing left)
        }

        // check if player is falling
        if (animator.GetBool("isFalling") != player.inAir) {
            animator.SetBool("isFalling", player.inAir); // set isFalling to be the same as inAir
        }

        // check if player is jumping
        if (animator.GetBool("isJumping") != player.jumpKeyWasPressed) {
            animator.SetBool("isJumping", player.jumpKeyWasPressed); // set isJumping to be the same as jumpKeyWasPressed
        }

        // check if player is dashing
        if (animator.GetBool("isDashing") != player.isDashing) {
            animator.SetBool("isDashing", player.isDashing);
        }

        // check if player is shooting
        if (animator.GetBool("isShooting") != player.isShooting) {
            animator.SetBool("isShooting", player.isShooting);
        }

        // activate the arms layer if shooting
        if (player.isShooting) {
            animator.SetLayerWeight(animator.GetLayerIndex("arms"), 1);
        } else {
            animator.SetLayerWeight(animator.GetLayerIndex("arms"), 0);   
        }
    }
}
