using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody rigidBodyComponent; // Set rigidbody to be used.
    public Transform groundCheckTransform = null; // Set transform for checking if player is touching the ground.

    // Jump vairables
    private int objectsPlayerIsTouching; // used to keep track of jumps and if the player is falling
    public bool jumpKeyWasPressed; // Keep track of whether jump key was pressed.
    private int jumpsTotal = 2; // Define *total* amount of jumps player can perform before having to touch grass again.
    private int jumpsRemaining; // Define *current* amount of jumps player can perform before having to touch grass again. (Defined on start event)
    private float jumpSpeed = 5.0f; // Define object's jump speed.
    public bool inAir; // used to control animations in animationStateController.cs

    // Movement variables
    public float horizontalInput; // Get horizontal input.
    private float walkSpeed = 4.0f; // Define object's walk speed.
    public GameObject footstepSound; // used for sound of footsteps


    // Start is called before the first frame update
    void Start() {
        rigidBodyComponent = GetComponent<Rigidbody>(); // Define rigidbody as object's rigidbody component.
        jumpsRemaining = jumpsTotal; // Set jumps remaining as total jumps.
        inAir = true; // the player starts out in the air

        // define the state of footstepSound
        if (footstepSound != null) {
            footstepSound.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update() {
        // pressing the space bar will trigger a jump
        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0) {
            jumpKeyWasPressed = true;
            inAir = true;
        }

        horizontalInput = Input.GetAxis("Horizontal") * walkSpeed; // Set horizontal direction to be input * walk speed.

        // footstep sound will start when the player is touching the ground and moving, and will stop otherwise
        if (Mathf.Abs(horizontalInput) > 0 && Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length > 2) {
            Footstep();
        }
        else {
            StopFootsteps();
        }
    }

    // FixedUpdate is called at a set rate (doesn't fluctuate based on frame rate like the Update method)
    private void FixedUpdate() {
        objectsPlayerIsTouching = Physics.OverlapSphere(groundCheckTransform.position, 0.05f).Length; // define how many objects player is touching
        rigidBodyComponent.velocity = new Vector3(horizontalInput, rigidBodyComponent.velocity.y, 0); // move the player left & right based off of horizontalInput

        // if the player has no jumps left and is not touching any other objects, return from the method
        if (objectsPlayerIsTouching <= 2 && jumpsRemaining <= 0) {
            return;
        }

        // If player touches ground, reset amount of jumps that can be performed.
        if (objectsPlayerIsTouching > 2) {
            jumpsRemaining = jumpsTotal; // Reset remaining jumps as total jumps.
            inAir = false;
        } else if (!inAir) {
            inAir = true; // player is in air if touching less than 2 objects
        }

        // make the player jump if the space bar was pressed
        if (jumpKeyWasPressed) {
            rigidBodyComponent.velocity = new Vector3(horizontalInput, 0, 0); // Cancel out gravity when peforming any kind of jump.
            rigidBodyComponent.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange); // Set upwards jump force as up vector * jump speed.
            jumpKeyWasPressed = false; // Define jump key as not pressed.
            jumpsRemaining--; // Decrese amount of jumps left to jump.
        }
    }

    // play the footstep sound
    void Footstep() {
        if (footstepSound != null && !footstepSound.activeSelf) {
            footstepSound.SetActive(true);
        }
    }

    // stop the footstep sound
    void StopFootsteps() {
        if (footstepSound != null && footstepSound.activeSelf) {
            footstepSound.SetActive(false);
        }
    }

}
