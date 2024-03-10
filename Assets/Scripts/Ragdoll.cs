using UnityEngine;
using UnityEngine.UIElements;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform ragdollRoot;
    private Rigidbody[] rigidbodies;
    private CharacterJoint[] joints;
    private Collider[] colliders;

    // gets all the necessary components for the ragdoll
    private void Awake() {
        rigidbodies = ragdollRoot.GetComponentsInChildren<Rigidbody>();
        joints = ragdollRoot.GetComponentsInChildren<CharacterJoint>();
        colliders = ragdollRoot.GetComponentsInChildren<Collider>();
        EnableAnimator();
    }

    // turns off the animator and starts the ragdoll (used in Die method in Player)
    public void EnableRagdoll() {
        animator.enabled = false;
        foreach (CharacterJoint joint in joints) {
            joint.enableCollision = true;
        }
        foreach (Collider collider in colliders) {
            collider.enabled = true;
        }
        foreach (Rigidbody rigidbody in rigidbodies) {
            rigidbody.velocity = Vector3.zero;
            rigidbody.detectCollisions = true;
            rigidbody.useGravity = true;
        }
    }

    // turns on the animator and disables the components for ragdoll
    void EnableAnimator() {
        animator.enabled = true;
        foreach (CharacterJoint joint in joints) {
            joint.enableCollision = false;
        }
        foreach (Collider collider in colliders) {
            collider.enabled = false;
        }
        foreach (Rigidbody rigidbody in rigidbodies) {
            rigidbody.detectCollisions = false;
            rigidbody.useGravity = false;
        }
    }
}
