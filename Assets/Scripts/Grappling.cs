using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling : MonoBehaviour {
    [Header("References")]
    //private PlayerMovementGrappling pm; // probably won't need this
    public Transform playerTransform;
    public Transform grappleStartPoint;
    public Vector3 mousePosition;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr; // need to assign this before testing

    [Header("Grappling")] // def change these to be private after deciding values
    public float maxGrappleDistance;
    public float grappleDelayTime;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    private float grapplingCd = 0; // no cooldown for now... change this later
    private float grapplingCdTimer;

    [Header("Input")]
    private KeyCode grappleKey = KeyCode.Mouse0;

    private bool grappling;


    private void Start() {
        //pm = GetComponent<PlayerMovementGrappling>();
    }

    private void Update() {
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 8));
        Debug.DrawRay(grappleStartPoint.position, mousePosition);

        // if the left mouse button is clicked, start grappling
        if(Input.GetKeyDown(grappleKey)) {
            StartGrapple();
        }

        // decrement the timer if there's time left on it
        if(grapplingCdTimer > 0) {
            grapplingCdTimer -= Time.deltaTime;
        }
    }

    private void StartGrapple() {
        // if the cool down timer is not zero, skip this method
        if(grapplingCdTimer > 0) {
            return;
        }

        grappling = true; // the player is now grappling


        RaycastHit hit;
        if(Physics.Raycast(playerTransform.position, mousePosition, out hit, maxGrappleDistance, whatIsGrappleable)) { // send a ray out from the camera
            grapplePoint = hit.point; // save the point that was hit in side of grapplePoint
            Invoke(nameof(ExecuteGrapple), grappleDelayTime); // run the ExecuteGrapple method after a small delay
        } else { // if the grappling hook doesn't hit anything, stop grappling
            grapplePoint = playerTransform.position * maxGrappleDistance;
            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        // draw a line to the grapplePoint
        lr.enabled = true;
        lr.SetPosition(1, grapplePoint);
    }

    private void ExecuteGrapple() {
        Debug.Log("Hit!");
        if (grappling) {
            lr.SetPosition(0, grappleStartPoint.position);
        }
    }

    private void StopGrapple() {
        Debug.Log("Miss!");
        grappling = false;
        grapplingCdTimer = grapplingCd;
        lr.enabled = false;
    }
}
