using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidBody;
    private bool collided;
    [SerializeField] private Transform vfxHit;//Ïú»ÙÌØÐ§
    [SerializeField] private float minDamage, maxDamage;
    private float attackDamage;
    public GameObject damageCanvas;
    private Vector3 up;
    private void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, 15f);
    }
    private void Start()
    {
        up = new Vector3(0, 0.8f, 0);
        //float speed = 10f;
       // bulletRigidBody.velocity = transform.forward*speed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Player")

            if (collision.gameObject.TryGetComponent<EnemyAI>(out EnemyAI enemyComponent))
            {
                attackDamage = Random.Range(minDamage, maxDamage);
                enemyComponent = collision.gameObject.GetComponent<EnemyAI>();
                enemyComponent.takeDamage(attackDamage);
                DamageNum damageble = Instantiate(damageCanvas, collision.transform.position+up, Quaternion.identity).GetComponent<DamageNum>();
                damageble.showUIDamage(Mathf.RoundToInt(attackDamage));
            }
        if (collision.gameObject.TryGetComponent<Turret>(out Turret turretComponent))
        {
            attackDamage = Random.Range(minDamage, maxDamage);
            turretComponent = collision.gameObject.GetComponent<Turret>();
            turretComponent.takeDamage(attackDamage);
            DamageNum damageble = Instantiate(damageCanvas, collision.transform.position + up, Quaternion.identity).GetComponent<DamageNum>();
            damageble.showUIDamage(Mathf.RoundToInt(attackDamage));
        }
        if (collision.gameObject.TryGetComponent<Boss>(out Boss bossComponent))
        {
            attackDamage = Random.Range(minDamage, maxDamage);
            bossComponent = collision.gameObject.GetComponent<Boss>();
            bossComponent.takeDamage(attackDamage);
            DamageNum damageble = Instantiate(damageCanvas, collision.transform.position + up, Quaternion.identity).GetComponent<DamageNum>();
            damageble.showUIDamage(Mathf.RoundToInt(attackDamage));
        }
        Destroy(gameObject);
            Instantiate(vfxHit, transform.position, Quaternion.identity);           
        }
    }


