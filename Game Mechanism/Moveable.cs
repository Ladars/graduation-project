using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    //public Transform wayPointTarget;
    public float movespeed;

    
    private void Start()
    {
        pointB.transform.position = transform.position;
    }
    private void FixedUpdate()
    {
        //Debug.Log(Vector3.Distance(transform.position, pointB.position));
        if (Vector3.Distance(transform.position, pointB.position) <=0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, movespeed * Time.deltaTime);
        } 

        if (Vector3.Distance(transform.position, pointA.position) <= 0.01f)
        {
           transform.position = Vector3.Slerp(transform.position, pointB.position, movespeed * Time.deltaTime);
        }
    }
}
