using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    //Damage Effect variable
    public SkinnedMeshRenderer skinnedMeshRenderer;
    
    public float blinkIntensity;
    public float blinkDuration;
    private float blinkTimer;
    public LayerMask playerMask;
    //reference
    private TurretRotate turretRotate;
    private Animator animator;
    private EnemySound enemySound;
    private BoxCollider boxCollider;
   

    // Start is called before the first frame update
    public bool PlayerInAttackRange;
    public Transform player;
    public float attckRange;
    public float health;
    public float maxHealth = 100f;
    //Health variable

    public Image redImage;//"Red" Health bar Image
    public Image whiteImage;//"White Effect" Health bar Image
    [SerializeField] private float hurtSpeed = 0.005f;
    //Damage Effect variable
    public TimeCounter timeCounter;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        enemySound = GetComponent<EnemySound>();
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Start()
    {
        timeCounter = FindObjectOfType<TimeCounter>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        health = maxHealth;
        turretRotate = GetComponentInChildren<TurretRotate>();
        player = GameObject.Find("Player").transform;
    }
  
  

    // Update is called once per frame
    void Update()
    {
        PlayerInAttackRange = Physics.CheckSphere(transform.position, attckRange, playerMask);

        healthBar();
        blinkEffect();
        // boomEffect.SetActive(true);
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
        skinnedMeshRenderer.material.color = Color.white * intensity;
    }
    public void takeDamage(float damage)
    {
        health -= damage;
        blinkTimer = blinkDuration;
        if (health <= 0)
        {
            timeCounter.eyeBatKillTime();
            //var projectileObj = Instantiate(boomEffect, transform.position, Quaternion.identity);
            animator.SetTrigger("isDie");
            enemySound.eyeBatDeathSoundEvent();
            Destroy(gameObject, 2f);
            boxCollider.enabled = false;
        }
    }
}
