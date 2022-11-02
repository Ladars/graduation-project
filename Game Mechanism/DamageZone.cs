using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    
  

    private void Start()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //player.takeDamage = true;

            Health health = other.GetComponent<Health>();
            health.takeDamage(1);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        //player.takeDamage = true;

    //        Health health = other.GetComponent<Health>();
    //        health.takeDamage(1);
    //    }
    //}

}
