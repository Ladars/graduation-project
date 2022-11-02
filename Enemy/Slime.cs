using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : EnemyAI
{
    private Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private protected override void Update()
    {
        base.Update();
        slimeDeath();
        if (alreadyAttacked)
        {
            animator.SetTrigger("isAttack");
        }
        //else
        //{
        //    animator.SetBool("isAttack", false);
        //}
    }
    public void FireBallAttack()
    {
        Rigidbody rb = Instantiate(projectile, shootingPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 5f, ForceMode.Impulse);
        rb.AddForce(transform.up * 5f, ForceMode.Impulse);
    }
    private void slimeDeath()
    {
        if (health <= 0)
        {
           
            animator.SetTrigger("isDie");
            agent.SetDestination(transform.position);
        }
    }
    private void getTime()
    {
        timeCounter.slimeKillTime();
    }

}
