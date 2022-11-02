using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTraceShoot : MonoBehaviour
{
    private Rigidbody bulletRigidBody;
    private bool collided;
    [SerializeField] private Transform vfxHit;//Ïú»ÙÌØÐ§
    //[SerializeField] private float minDamage, maxDamage;
    private float attackDamage;

    private Vector3 up;
    private Transform player;
    [SerializeField] private float speed;
    [SerializeField] private float followTime=1.5f;
    [SerializeField] private float gravityFactor;
    private void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, 15f);
        player = GameObject.Find("Player").transform;
        up = new Vector3(0, 0.5f, 0);
    }
    private void Update()
    {
      
        followTime -= Time.deltaTime;
        followPlayer();
    }
    private void FixedUpdate()
    {
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Bullet" && collision.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
            Instantiate(vfxHit, transform.position, Quaternion.identity);
            if (collision.gameObject.TryGetComponent<Health>(out Health playerHealth))
            {
                playerHealth = collision.gameObject.GetComponent<Health>();
                playerHealth.takeDamageFromEnemy(1);

            }

        }
    }
    private void followPlayer()
    {
        if (followTime >= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position + up, speed * Time.deltaTime);
        }
        bulletRigidBody.AddForce(Physics.gravity * gravityFactor, ForceMode.Acceleration);
    }
}
