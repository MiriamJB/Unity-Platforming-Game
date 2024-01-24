using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour {

    [SerializeField] public Transform groundCheckTransform = null;
    [SerializeField] public LayerMask playerMask;
    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;

    // Start is called before the first frame update
    void Start() {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            jumpKeyWasPressed = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space)) {
            jumpKeyWasPressed = false;
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate() {
        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) {
            return;
        }

        if (jumpKeyWasPressed) {
            rigidbodyComponent.AddForce(Vector3.up * 5, ForceMode.VelocityChange);
        }
    }
}
