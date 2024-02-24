using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleMovement : MonoBehaviour
{
    public Rigidbody rb;
    private Vector3 velocityToSet;

    public void JumpToPosition(Vector3 targetPosition, float trajectoryHeight) {
        rb.velocity = CalculateJumpVelocity(transform.position, targetPosition, trajectoryHeight);
        Invoke(nameof(SetVelocity), 0.1f);
    }

    private void SetVelocity() {
        rb.velocity = velocityToSet;
    }

    public Vector3 CalculateJumpVelocity(Vector3 startPoint, Vector3 endPoint, float trajectoryHeight) {
    float gravity = Physics.gravity.y;
    float displacementY = endPoint.y - startPoint.y;
    Vector3 displacementXZ = new Vector3(endPoint.x - startPoint.x, 0f, endPoint.z - startPoint.z);

    Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * trajectoryHeight);
    Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * trajectoryHeight / gravity)
        + Mathf.Sqrt(2 * (displacementY - trajectoryHeight) / gravity));

    return velocityXZ + velocityY;
   }
}
