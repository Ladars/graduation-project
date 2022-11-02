using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask groundMask, playerMask;
    //Patrol variable
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    //Attack variable
    public float attckInterval;
    public bool alreadyAttacked;
    public Transform shootingPoint;
    //States variable
    public float sightRange, attckRange;
    public bool playerInSightRange, PlayerInAttackRange;
    //projectile
    public GameObject projectile;
    //Health variable
    public float health, maxHealth = 50f;   
    public Image hpImage;//"Red" Health bar Image
    public Image hpEffectImage;//"White Effect" Health bar Image
    [SerializeField] private float hurtSpeed = 0.005f;
    //Damage Effect variable
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public float blinkIntensity;
    public float blinkDuration;
    private float blinkTimer;
    //counter
    public TimeCounter timeCounter;
    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth;
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        timeCounter = FindObjectOfType<TimeCounter>();
    }
    private void Start()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        
    }

    protected virtual private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position,sightRange,playerMask);
        PlayerInAttackRange = Physics.CheckSphere(transform.position,attckRange,playerMask);

        if(!playerInSightRange && !PlayerInAttackRange)
        {
            Patroling();
        }
        if(playerInSightRange && !PlayerInAttackRange)
        {
            ChasePlayer();
        }
        if (playerInSightRange && PlayerInAttackRange)
        {
            AttackPlayer();
        }
        healthBar();
        blinkEffect();
    }
    private void Patroling()
    {
        if (!walkPointSet) 
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 2)//超出距离判断
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()//在一定范围内随机走动
    {
        float randomZ = Random.Range(-walkPointRange,walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundMask))//向下发射射线检测地面
        {
            walkPointSet = true;
        }
    }
   
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);//使敌人停下
        if (health > 0)
        {
            transform.LookAt(player);
        }
        if (!alreadyAttacked)
        {            
            Invoke(nameof(ResetAttack), attckInterval);
            alreadyAttacked = true;
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    public void takeDamage(float damage)
    {
        health -= damage;
        blinkTimer = blinkDuration;
        if(health <= 0)
        {
            DestroyEnemy();
        }
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject,3);
    }
    private void healthBar()
    {
        hpImage.fillAmount = health / maxHealth;  //制作掉血效果  红色的血条表示当前生命值，以当前生命值和最大生命值的比来显示

        if (hpEffectImage.fillAmount > hpImage.fillAmount)
        {
            hpEffectImage.fillAmount -= hurtSpeed;
        }
        else
        {
            hpEffectImage.fillAmount = hpImage.fillAmount;
        }
    }
    private void blinkEffect()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity)+1;
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }
}
