using UnityEngine;

public class Grappling : MonoBehaviour
{
    // player/mouse info
    public Transform playerTransform; // used to get the position of the player. Swap this out with the grapple gun
    private Vector3 startPoint; // where the line for the grappling hook will start
    private Vector3 mousePos; // where the cursor is in the game view

    // grappling specifications
    public LineRenderer line; // used to draw the line for the grappling hook
    public LayerMask whatIsGrappleable; // the layer that the grappling hook can attach to
    private readonly int maxGrappleDistance = 5; // the maximum distance the player can shoot their grappling hook
    private Vector3 grapplePoint; // where the line for the grappling hook will end
    private readonly KeyCode grappleKey = KeyCode.Mouse0; // left click

    // joint stuff
    private SpringJoint joint;
    private readonly float spring = 4.5f;
    private readonly float damper = 7f;
    private readonly float massScale = 4.5f;


    void Start() {
        line.enabled = false; // starts without a line being drawn
    }

    void Update() {
        startPoint = playerTransform.position; // update where the line should start based on the player's position

        if (Input.GetKeyUp(grappleKey)) { // check if no longer grappling
            StopGrapple();
        } else if (line.enabled) { // check if still grappling
            ExecuteGrapple();
        } else if (Input.GetKeyDown(grappleKey)) { //if not grappling, check to see if the user has pressed the key to start grappling
            StartGrapple();
        }
    }
    
    // see if the user will hit anything when they start grappling
    private void StartGrapple() {
        RaycastHit hit; // used to store the data of where the grappling hook lands a hit
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 8f)); // find where the mouse cursor is in the game view

        if(Physics.Raycast(startPoint, mousePos-startPoint, out hit, maxGrappleDistance, whatIsGrappleable)) { // send out a ray to check if there is anything grappleable between the player and the mouse cursor
            grapplePoint = hit.point; // set the grappling point to the point that was hit
            ConnectJoint();
            ExecuteGrapple();
        } else { // if there was no hit, then the user missed
            Vector3 dir = mousePos - startPoint; // find the direction the mouse is pointing to
            float dist = Mathf.Clamp(Vector3.Distance(startPoint, mousePos), 0, maxGrappleDistance); // find the distance between the player and the mouse, and go no further than the max grapple distance
            grapplePoint = startPoint + dir.normalized * dist; // set the grappling point to be the at the distance of dist relative to the start point
            DrawGrapplingHook();
            Invoke(nameof(StopGrapple), 0.2f);
        }

    }

    // create the joint for the grappling hook
    private void ConnectJoint() {
        joint = playerTransform.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grapplePoint;

        float distanceFromPoint = Vector3.Distance(startPoint, grapplePoint);

        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        joint.spring = spring;
        joint.damper = damper;
        joint.massScale = massScale;
    }

    // draw the line and stop grappling if the user releases the grappleKey
    private void ExecuteGrapple() {
        DrawGrapplingHook();
    }

    // stop drawing the line and destroy the joint
    private void StopGrapple() {
        line.enabled = false;
        Destroy(joint);
    }

    // draw a line from the start point to the grapple point
    private void DrawGrapplingHook() {
        line.enabled = true;
        line.SetPosition(0, startPoint);
        line.SetPosition(1, grapplePoint);
    }
}
