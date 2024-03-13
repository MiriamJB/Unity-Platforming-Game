using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    public Transform[] points;
    int current;
    public float speed;
    public bool cycle = true;
    private bool stop = false;

    // Start is called before the first frame update
    void Start()
    {
        current = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != points[current].position) {
            if (!stop) {
                transform.position = Vector3.MoveTowards(transform.position, points[current].position, speed * Time.deltaTime);
            }
        }
        // If cycle, object will go towards first point after reaching the final one.
        else
        {
            if (cycle) {
                current = (current + 1) % points.Length;
            }
            else {
                if (current + 1 == points.Length) {
                    stop = true;
                }
                else {
                    current = (current + 1) % points.Length;
                }
            }
        }
    }
}
