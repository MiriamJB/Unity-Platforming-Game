using UnityEngine;
using System.Collections;

public class GrapplingGun : MonoBehaviour {

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, player;

    public float spring = 4.5f;

    public float damper = 7f;

    public float massScale = 4.5f;

    private float maxDistance = 30f;
    private SpringJoint joint;

    void Awake() {
        lr = GetComponent<LineRenderer>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0)) {
            StopGrapple();
        }
    }

    void LateUpdate() {
        DrawRope();
    }

    void StartGrapple() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    
        if (Physics.Raycast(ray, out hit, maxDistance, whatIsGrappleable)) {

            grapplePoint = hit.point; 

            Vector3 ropeStart = gunTip.position;
            Vector3 ropeEnd = grapplePoint;

            StartCoroutine(ExtendRope(ropeStart, ropeEnd));
        }
    }

    IEnumerator ExtendRope(Vector3 start, Vector3 end) {
        float elapsedTime = 0f;
        float duration = 0.5f;

        while (elapsedTime < duration) {
            Vector3 currentPos = Vector3.Lerp(start, end, elapsedTime / duration);

            lr.positionCount = 2;
            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, currentPos);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        lr.positionCount = 2;
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, end);

        ApplyGrappleLogic();
    }

    void ApplyGrappleLogic() {
        joint = player.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grapplePoint;

        float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

        joint.maxDistance = distanceFromPoint * 0.8f;
        joint.minDistance = distanceFromPoint * 0.25f;

        joint.spring = spring;
        joint.damper = damper;
        joint.massScale = massScale;
    }

    void StopGrapple() {
        lr.positionCount = 0;
        Destroy(joint);
    }

    void DrawRope() {
        if (!joint) return;

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }
}
