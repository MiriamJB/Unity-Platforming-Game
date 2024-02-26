using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirDash : MonoBehaviour
{
    private Rigidbody rb;
    private int dashesLeft;
    private Color originalColor;
    [SerializeField] private bool isGrounded;
    [SerializeField] private Transform groundRayTransform;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private int numDashes;
    [SerializeField] private int dashForce;
    [SerializeField] private ParticleSystem dashTrail;

    //[SerializeField] private SpriteRenderer characterRenderer;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        //originalColor = characterRenderer.color;
        dashTrail = GetComponentInChildren<ParticleSystem>(); // Assumes the Particle System is a child of the player
        dashTrail.Stop(); // Stop the particle system initially
    }

    void FixedUpdate()
    {
        isGrounded = Physics.Linecast(groundRayTransform.position, groundRayTransform.position, groundLayer);
        if (isGrounded)
        {
            dashesLeft = numDashes;
        }
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        if (  x != 0 && isGrounded == false && rb.velocity.y <= 0)
        {
            if (dashesLeft > 0)
            {
                StartCoroutine(Dash());
            }
        }
    }

    IEnumerator Dash()
    {
        Debug.Log("Dashing!");
    //characterRenderer.color = Color.cyan;

    // Play the dash trail
    dashTrail.Play();

    Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    rb.velocity = Vector3.zero;
    rb.AddForce(dir.normalized * dashForce, ForceMode.VelocityChange);
    dashesLeft--;

    yield return new WaitForSeconds(0.2f);

    // Stop the dash trail
    dashTrail.Stop();

    //characterRenderer.color = originalColor;
    }
}
