using System.Collections;
using System.Collections.Generic;
using PlasticGui.WorkspaceWindow;
using UnityEngine;

public class Grappling2 : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 mousePos;
    public LineRenderer line;
    //public HingeJoint grappleHinge;

    private int maxGrappleDistance = 4;
    public LayerMask whatIsGrappleable;
    private Vector3 grapplePoint;
    private KeyCode grappleKey = KeyCode.Mouse0; // left click

    // Start is called before the first frame update
    void Start()
    {
        line.enabled = false;
    }

    // Update is called once per frame
    void Update(){
        // debug stuff below:
        // mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 8f));
        // Debug.DrawRay(playerTransform.position, mousePos-playerTransform.position);

        if (line.enabled) {
            ExecuteGrapple();
        } else if (Input.GetKeyDown(grappleKey)) {
            StartGrapple();
        }
        
    }

    private void StartGrapple() {
        RaycastHit hit;
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 8f));

        if(Physics.Raycast(playerTransform.position, mousePos-playerTransform.position, out hit, maxGrappleDistance, whatIsGrappleable)) {
            Debug.Log("hit");
            grapplePoint = hit.point;
            //grappleHinge.anchor = grapplePoint;
            ExecuteGrapple();
        } else {
            Debug.Log("miss");
            grapplePoint = findMaxDistance();
            DrawGrapplingHook();
            Invoke(nameof(StopGrapple), 0.2f);
        }

    }

    private void ExecuteGrapple() {
        DrawGrapplingHook();

        if (Input.GetKeyUp(grappleKey)) {
            StopGrapple();
        }
    }

    private void StopGrapple() {
        line.enabled = false;
    }

    private void DrawGrapplingHook() {
        line.enabled = true;
        line.SetPosition(0, playerTransform.position);
        line.SetPosition(1, grapplePoint);
    }

    // find the coordinates for the grapple point based off of the players postion, the mouse position, and the max grapple distance
    // it mostly works...
    private Vector3 findMaxDistance() {
        Vector3 vector = mousePos;
        float xDistance = mousePos.x - playerTransform.position.x;
        float yDistance = mousePos.y - playerTransform.position.y;

        // find x coordinate
        if(xDistance < -maxGrappleDistance) {
            vector.x = playerTransform.position.x - maxGrappleDistance;
        } else if (xDistance > maxGrappleDistance) {
            vector.x = playerTransform.position.x + maxGrappleDistance;
        }

        // find y coordinate
        if(yDistance < -maxGrappleDistance) {
            vector.y = playerTransform.position.y - maxGrappleDistance;
        } else if (yDistance > maxGrappleDistance) {
            vector.y = playerTransform.position.y + maxGrappleDistance;
        }

        return vector;
    }
}
