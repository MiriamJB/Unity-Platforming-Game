using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    Player moveScript;
   public float dashSpeed;
   public float dashTime;

    void Start()
    {
        moveScript = GetComponent<Player>();
       
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            
                StartCoroutine(Dash());
           
        }
    }

    IEnumerator Dash()
    {
        Debug.Log("Dashing!");
    //characterRenderer.color = Color.cyan;

    float startTime = Time.time;
    while(Time.time < startTime + dashTime)
    {
        moveScript.controller.Move(moveScript.moveDir * dashSpeed * dashTime);
    }
    
    }
}
