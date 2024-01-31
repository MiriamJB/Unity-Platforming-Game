using UnityEngine;

public class Player : MonoBehaviour
{
    // Set transform for checking if player is touching the ground.
    public Transform groundCheckTransform = null;
    // Keep track of wheter jump key was pressed.
    private bool jumpKeyWasPressed;
    // Get horizontal input.
    private float horizontalInput;
    // Set rigidbody to be used.
    private Rigidbody rigidBodyComponent;

    // Define *total* amount of jumps player can perform before having to touch grass again.
    private int jumpsTotal = 2;
    // Define *current* amount of jumps player can perform before having to touch grass again. (Defined on start event)
    private int jumpsRemaining;
    // Define object's walk speed.
    private float walkSpeed = 4.0f;
    // Define object's jump speed.
    private float jumpSpeed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Define rigidbody as object's rigidbody component.
        rigidBodyComponent = GetComponent<Rigidbody>();
        // Set jumps remaining as total jumps.
        jumpsRemaining = jumpsTotal;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            jumpKeyWasPressed = true;
        }
        // Set horizontal direction to be input * walk speed.
        horizontalInput = Input.GetAxis("Horizontal")*walkSpeed;
    }

    private void FixedUpdate()
    {
        rigidBodyComponent.velocity = new Vector3(horizontalInput, rigidBodyComponent.velocity.y , 0);
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length <= 2 && jumpsRemaining <= 0)
        {
            return;
        }
        // If player touches ground, reset amount of jumps that can be performed.
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f).Length > 2)
        {
            // Reset remaining jumps as total jumps.
            jumpsRemaining = jumpsTotal;
        }
        
        if (jumpKeyWasPressed)
        {
            // Cancel out gravity when peforming any kind of jump.
            rigidBodyComponent.velocity = new Vector3(horizontalInput, 0 , 0);
            // Set upwards jump force as up vector * jump speed.
            rigidBodyComponent.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
            // Define jump key as not pressed.
            jumpKeyWasPressed = false;
            // Decrese amount of jumps left to jump.
            jumpsRemaining--;
        }

    }
}  
