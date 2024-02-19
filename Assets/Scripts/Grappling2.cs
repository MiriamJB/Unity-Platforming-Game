using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling2 : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 mousePos;
    public LineRenderer line;

    private int maxGrappleDistance = 2;
    public LayerMask whatIsGrappleable;
    private Vector3 grapplePoint;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update(){
        //mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 8));
        //Debug.DrawRay(playerTransform.position, mousePos);

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            StartGrapple();
        }
        
    }

    private void StartGrapple() {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 8f));

        RaycastHit hit;
        if(Physics.Raycast(playerTransform.position, mousePos, out hit, maxGrappleDistance, whatIsGrappleable)) {
            grapplePoint = hit.point;
            ExecuteGrapple();
        } else {
            StopGrapple();
        }

    }

    private void ExecuteGrapple() {
        Debug.Log("hit");
        line.SetPosition(0, playerTransform.position);
        line.SetPosition(1, grapplePoint);
    }

    private void StopGrapple() {
        Debug.Log("miss");
        line.SetPosition(0, playerTransform.position);
        line.SetPosition(1, mousePos);
    }
}
