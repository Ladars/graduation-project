using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private Rigidbody bulletRigidBody;
    private bool collided;
    [SerializeField] private Transform vfxHit;
    private void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
        Destroy(gameObject, 15f);
    }
    private void Start()
    {
        //float speed = 10f;
        // bulletRigidBody.velocity = transform.forward*speed;
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
}
