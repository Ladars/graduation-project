using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogKnight : EnemyAI
{
    private Animator animator;
    private BoxCollider boxCollider;
    private void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }
    private protected override void Update()
    {
        base.Update();
        dogKnightDeath();
        dogKnightAttack();
        dogKnightMove();
    }
    private void dogKnightDeath()
    {
        if (health <= 0)
        {

            animator.SetTrigger("isDie");
            agent.SetDestination(transform.position);
        }
    }
    private void dogKnightAttack()
    {
        if (alreadyAttacked)
        {
            
            agent.SetDestination(transform.position);//使敌人停下
            animator.SetBool("isAttack", true);
        }
        if (!alreadyAttacked)
        {
            //boxCollider.enabled = false;
            //agent.SetDestination(player.position);
            animator.SetBool("isAttack", false);
        }
    }
    private void dogKnightMove()
    {
        if (playerInSightRange && !PlayerInAttackRange)
        {
            animator.SetBool("isMove", true);
        }
        if (playerInSightRange && PlayerInAttackRange)
        {
            animator.SetBool("isMove", false);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent<Health>(out Health playerHealth))
            {
                playerHealth = other.gameObject.GetComponent<Health>();
                playerHealth.takeDamageFromEnemy(1);
            }
        }
    }
    IEnumerator attckInvoke()
    {
        yield return new WaitForSeconds(0.5f);
        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        boxCollider.enabled = false;
    }
    private void attackDamage()//使玩家收到伤害
    {
        StartCoroutine(attckInvoke());
    }
    private void getTime()
    {
        timeCounter.dogKnightKillTime();
    }
}
