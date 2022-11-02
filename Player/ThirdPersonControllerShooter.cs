using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class ThirdPersonControllerShooter : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimCamera;
    PlayerMovement playerMovement;
    private Transform cameraTransform;
    [SerializeField] private float rotationSpeed=1;
    public GameObject reticle;
   // [SerializeField] private GameObject bulletprefab;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private Transform prefabBulletProjectile;

    [SerializeField] private LayerMask aimColliderLayerMusk = new LayerMask();
    public Camera cam;
    private Vector3 destination;
    //public BulletController bulletController;

    private float timeToFire;
    public float fireRate = 4; //射击频率
    public ParticleSystem muzzleEffect;

    [SerializeField] private Transform aimTarget;
    [SerializeField] private float aimDistance;

    private RigBuilder rigBuilder;
    [SerializeField] private GameObject weapon;

    private CinemachineShake cameraShake;
    private PlayerSoundEffect soundEffect;

    private Quaternion resetRotation;
    private bool IsResetRotation;
    private void Start()
    {
        resetRotation = Quaternion.Euler(0, 0, 0);
        rigBuilder = GetComponent<RigBuilder>();
        rigBuilder.layers[0].active = false;
        rigBuilder.layers[1].active = false;
        weapon.SetActive(false);
        playerMovement = GetComponent<PlayerMovement>();
        cameraTransform = Camera.main.transform;
        //bulletController = GetComponent<BulletController>();
        cameraShake = FindObjectOfType<CinemachineShake>();
        soundEffect = FindObjectOfType<PlayerSoundEffect>();
    }
   
    private void Aim()
    {
        if (playerMovement.isAimPressed)
        {
          playerMovement.Aiming = true;
            playerMovement.aimMove();
            aimCamera.gameObject.SetActive(true);    
            reticle.SetActive(true);
            rigBuilder.layers[0].active = true;
            rigBuilder.layers[1].active = true;
            weapon.SetActive(true);

            camToward();
          
            if (playerMovement.isShootPressed&&Time.time>=timeToFire)
            {
                //ShootGun();
                cameraShake.shakeCamera(0.6f,0.25f);
                timeToFire = Time.time + 1 / fireRate;
                ShootProjectile();
                muzzleEffect.Play();
                soundEffect.shootSoundEvent();
                //Vector3 aimDir = (mouseWorldPosition-muzzle.position).normalized;
                //Instantiate(prefabBulletProjectile, muzzle.position, Quaternion.LookRotation(aimDir, Vector3.left));
            }
        }
        else
        {

             aimCamera.gameObject.SetActive(false);
            weapon.SetActive(false);
            reticle.SetActive(false);
            rigBuilder.layers[0].active = false;
            rigBuilder.layers[1].active = false;
            playerMovement.Aiming = false;
        }
    }
    private void Update()
    {
        aimTarget.position = cam.transform.position + cam.transform.forward * aimDistance;
        Aim();
    }
    private void camToward()//使人物朝向瞄准方向
    {
        float targetAngle = cameraTransform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0,targetAngle,0);
        transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation,rotationSpeed*Time.deltaTime);
    }
    void ShootProjectile()//子弹射击
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }
        instantiateProjectile();
        Instantiate(muzzleEffect, muzzle.position, muzzle.rotation);

    }
    void instantiateProjectile()
    {
        var projectileObj = Instantiate(prefabBulletProjectile, muzzle.position, Quaternion.identity) ;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination-muzzle.position).normalized*30f;
    }





    //void ShootGun()//子弹射击
    //{
    //    RaycastHit hit;
    //    BulletController bulletController = GetComponent<BulletController>();
    //    GameObject bullet = GameObject.Instantiate(bulletprefab, muzzle.position, Quaternion.identity, bulletParent);
    //    if (Physics.Raycast(cameraTransform.position,cameraTransform.forward,out hit, Mathf.Infinity)) 
    //    {          
    //        bulletController.target = hit.point;
    //        bulletController.hit = true;
    //    }
    //    else
    //    {
    //        bulletController.target = cameraTransform.position+cameraTransform.forward*25f;
    //        bulletController.hit = true;
    //    }
    //}
    //Vector3 mouseWorldPosition = Vector3.zero;
    //Vector2 screenCenterPoint = new Vector2(Screen.width / 2, Screen.height / 2);
    //Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
    // Vector3 aimDirection =()
    //if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMusk))
    //{
    //    mouseWorldPosition = raycastHit.point;
    //}

}
