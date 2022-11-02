using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Turret : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject projectile;
    public Transform shootingPoint;
    public bool PlayerInAttackRange;
    public Transform player;
    public float attckRange;
    private bool alreadyAttacked;
    public float attckInterval;
    private float health;
    public float maxHealth = 100f;
    //Health variable
  
    public Image redImage;//"Red" Health bar Image
    public Image whiteImage;//"White Effect" Health bar Image
    [SerializeField] private float hurtSpeed = 0.005f;
    //Damage Effect variable
    //public SkinnedMeshRenderer skinnedMeshRenderer;
    public MeshRenderer meshRenderer;
    public float blinkIntensity;
    public float blinkDuration;
    private float blinkTimer;
    public LayerMask playerMask;
    //reference
    private TurretRotate turretRotate;
    private Animator animator;
    private EnemySound enemySound;
    private BoxCollider boxCollider;
    //particle
    public GameObject boomEffect;
    public GameObject fireEffect;
    public TimeCounter timeCounter;
    private void Awake()
    {
        //skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
       
        animator = GetComponent<Animator>();
        enemySound = GetComponent<EnemySound>();
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
       meshRenderer = GetComponentInChildren<MeshRenderer>();
        health = maxHealth;
        turretRotate = GetComponentInChildren<TurretRotate>();
        player = GameObject.Find("Player").transform;
        timeCounter = FindObjectOfType<TimeCounter>();
    }
    public void TurretAttack()
    {
        if (health >= 0)
        {
            Rigidbody rb = Instantiate(projectile, shootingPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(shootingPoint.forward * 20f, ForceMode.Impulse);
        }
       
        //rb.AddForce(transform.up * 5f, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInAttackRange = Physics.CheckSphere(transform.position, attckRange, playerMask);
        if (PlayerInAttackRange)
        {
            AttackPlayer();
        }
        healthBar();
        blinkEffect();
       // boomEffect.SetActive(true);
    }
    private void AttackPlayer()
    {

        if (health > 0)
        {
            //transform.LookAt(player);
            turretRotate.rotate();
        }
        if (!alreadyAttacked&&health>0)
        {

            enemySound.turretAttackSoundEvnet();
            Invoke(nameof(ResetAttack), attckInterval);
            alreadyAttacked = true;            
        }

    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
        TurretAttack();
        
    }
    private void healthBar()
    {
        redImage.fillAmount = health / maxHealth;  //制作掉血效果  红色的血条表示当前生命值，以当前生命值和最大生命值的比来显示

        if (whiteImage.fillAmount > redImage.fillAmount)
        {
            whiteImage.fillAmount -= hurtSpeed;
        }
        else
        {
            whiteImage.fillAmount = redImage.fillAmount;
        }
    }
    private void blinkEffect()
    {
        blinkTimer -= Time.deltaTime;
        float lerp = Mathf.Clamp01(blinkTimer / blinkDuration);
        float intensity = (lerp * blinkIntensity) + 1;
        meshRenderer.material.color = Color.white * intensity;
    }
    public void takeDamage(float damage)
    {
        health -= damage;
        blinkTimer = blinkDuration;
        if (health <= 0)
        {
            timeCounter.turretKillTime();
            //var projectileObj = Instantiate(boomEffect, transform.position, Quaternion.identity);
            boomEffect.SetActive(true);
            fireEffect.SetActive(true);
            enemySound.turretDeathSoundEvnet();
            Destroy(gameObject, 2f);
            boxCollider.enabled = false;
        }
    }
}
