using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Rigidbody rigidBodyComponent; // Set rigidbody to be used.
    public Ragdoll ragdoll;

    // Jump vairables
    public Transform groundCheckTransform; // Set transform for checking if player is touching the ground.
    private int objectsPlayerIsTouching; // used to keep track of jumps and if the player is falling
    public bool jumpKeyWasPressed; // Keep track of whether jump key was pressed.
    private int jumpsTotal = 2; // Define *total* amount of jumps player can perform before having to touch grass again.
    private int jumpsRemaining; // Define *current* amount of jumps player can perform before having to touch grass again. (Defined on start event)
    private float jumpSpeed = 5.0f; // Define object's jump speed.

    // Movement variables
    public float horizontalInput; // Get horizontal input.
    private float walkSpeed = 4.0f; // Define object's walk speed.
    public GameObject footstepSound; // used for sound of footsteps
    public GameObject tutorialText;

    // movement/direction booleans
    public bool inAir = true; // used to control animations in animationStateController.cs
    public bool isDashing = false; // controled in inAirDash
    public bool isFacingRight = true; // public so it can be accessed by the shooting mechanism & animator
    public bool isShooting = false; // shooting boolean for the animator
    public bool canMove = true; // switches to false when dead

    // Others
    public bool invulnerability = false;
    public bool isNearSign = false;
    public int timeToRestart; // amount of time to wait after death before restarting the scene
    public ParticleSystem dustEffect; // particle system for dust effect


    public ParticleSystem dust;

    // Start is called before the first frame update
    void Start()
    {
        rigidBodyComponent = GetComponent<Rigidbody>(); // Define rigidbody as object's rigidbody component.
        jumpsRemaining = jumpsTotal; // Set jumps remaining as total jumps.

        // define the state of footstepSound
        if (footstepSound != null) {
            footstepSound.SetActive(false);
        }

        if (tutorialText != null) {
            tutorialText.SetActive(false);
        }
        // Update tutorial text visibility based on the player's proximity to the sign
        else {
            if (tutorialText != null) {
                tutorialText.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) {
            return;
        }

        // pressing the space bar will trigger a jump
        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0) {
            jumpKeyWasPressed = true;
            inAir = true;
        }

        horizontalInput = Input.GetAxis("Horizontal") * walkSpeed; // Set horizontal direction to be input * walk speed.
        // Update direction based on horizontal input
        if (horizontalInput > 0) {
            isFacingRight = true;
        } else if (horizontalInput < 0) {
            isFacingRight = false;
        }

        // footstep sound will start when the player is touching the ground and moving, and will stop otherwise
        if (Mathf.Abs(horizontalInput) > 0 && Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length > 2)
        {
            Footstep();
            CreateDust();
        }
        else
        {
            StopFootsteps();
        }
        
    }

    // FixedUpdate is called at a set rate (doesn't fluctuate based on frame rate like the Update method)
    private void FixedUpdate()
    {
        if (!canMove) {
            return;
        }

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
            CreateDust(); // Trigger dust effect when jumping.
        }
    }
    /* dust effect */
    void CreateDust() {
            dustEffect.Play();
    }

    /* FOOTSTEP SOUND */
    void Footstep() {
        if (footstepSound != null && !footstepSound.activeSelf) {
            footstepSound.SetActive(true);
        }
    }

    void StopFootsteps() {
        if (footstepSound != null && footstepSound.activeSelf) {
            footstepSound.SetActive(false);
        }
    }

    /* INVULNERABILITY */
    public void ActivateInvulnerability() {
        invulnerability = true;
        Invoke("DeactivateInvulnerability", 3f);
    }

    private void DeactivateInvulnerability() {
        invulnerability = false;
    }

    /* DEATH */
    public void Die() {
        DisableMovement();
        Camera.main.transform.SetParent(null); // detach the camera from the player
        ragdoll.EnableRagdoll(); // make the player ragdoll
        Invoke(nameof(ReloadScene), timeToRestart); // reload after a few seconds
    }

    public void DisableMovement() {
        canMove = false; // lock the character so the user cannot move them anymore
        StopFootsteps(); // stop the footstep sound
    }

    void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /* SIGNS */
    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Sign")) {
            isNearSign = true;
        }
    }

    // OnTriggerExit is called when the Collider other exits the trigger
    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Sign")) {
            isNearSign = false;
        }
    }

    void CreateDust(){

        dust.Play();
    }

}