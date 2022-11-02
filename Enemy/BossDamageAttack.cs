using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageAttack : MonoBehaviour
{
    //±¬Õ¨¹¥»÷
    public GameObject explosionArea;
    public int minXPos,maxXPos;
    public int minZPos,maxZpos;
    public float spawnTime = 5f;
    private float spawnCounter;

    //×·×Ù¹¥»÷
    private Transform player;
    private float speed = 2;
    public GameObject tracerShotBullet;
    public Transform shootingPoint;
    private float attackCounter;
    public float attackInterval;
    private Quaternion rotation;
    //Ðý×ª
    [SerializeField] private Transform eye;
    [SerializeField] private Transform point;
    //reference
    private Boss boss;
    private EnemySound enemySound;
    private void Start()
    {
        player = GameObject.Find("Player").transform;
        //rotation = new Quaternion(90, 0, 0,0);
        boss = GetComponent<Boss>();
        enemySound = GetComponent<EnemySound>();
    }
    public void spawn()
    {
        
        if (spawnCounter <0)
        {
            spawnCounter += spawnTime;
           
            
                Vector3 spawnPosition = new Vector3(Random.Range(minXPos, maxXPos), 25, Random.Range(minZPos, maxZpos));
                Instantiate(explosionArea, spawnPosition, Quaternion.identity);
            
            
        }
    }
    private void Update()
    {

        //spawn();
        if (spawnCounter > -0.1)
        {
            spawnCounter -= Time.deltaTime;
        }
       
        if (attackCounter > -0.1)
        {
            attackCounter -= Time.deltaTime;
        }
       
        tracerShot();
        BosslookAtPlayer();


    }
    private void tracerShot()
    {
        if (boss.PlayerInAttackRange&&attackCounter<0&&boss.health>0)
        {
            attackCounter += attackInterval;
            enemySound.eyeBatAttackSoundEvent();
            Rigidbody rb = Instantiate(tracerShotBullet, point.position, point.rotation).GetComponent<Rigidbody>();
            rb.AddForce(point.forward * 35f, ForceMode.Impulse);
        }     
    }
    private void BosslookAtPlayer()
    {
        eye.transform.LookAt(player);
        point.LookAt(player);
    }
}
