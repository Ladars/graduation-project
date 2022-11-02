using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotate : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;

    }

    // Update is called once per frame
    public void rotate()
    {
        transform.LookAt(player);
    }
}
