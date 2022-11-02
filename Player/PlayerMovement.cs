using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;


public class PlayerMovement : MonoBehaviour
    {
    // reference variables 
    public float moveSpeed;
    Animator animator;
    PlayerInput playerInput;
    private Camera mainCam;
    CharacterController characterController;
    BoxCollider boxCollider;

    //hash variables
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int _jumpCountHash;


    //�洢��ɫ���������
    Vector3 currentMovementInput;
    Vector3 Movement;
    Vector3 runMovement;
    Vector3 currentMovement;
    Vector3 currentRunMovement;
    bool isMovementPressed;
    bool isRunPressed;
    Vector3 FinalMovement;
    Vector3 FinalRunMovement;

    //constants ����
    float rotationFactorPerFrame = 15f;

    //gravity variables
    [SerializeField]float _gravity = -9.8f;
    float _groundedGravity = -0.05f;
    Vector3 fallenSpeed;
    

    //jumping variables 
    bool isJumpPressed = false;
    
    public float jumpForce;
    public float jumpMaxTime = 5f;
    private float jumpTime;

    [SerializeField] private bool isGounded;
    [SerializeField] private float checkedRadius;
    public LayerMask layerMask;

    public bool interation ; //��ҶԻ�ʱ�����ƶ�

    public DamageZone damageZone;
    public bool takeDamage=true;
    //��Ч  Sound Effect
    //public AudioManager audioManager;

    //Aim variables
    public bool isAimPressed = false;
    public bool Aiming;//����׼ʱ���ɱ��ܺ���Ծ
    //Shoot variables
    public bool isShootPressed = false;
    //ground Detect
   
    private void Awake()
    {
        jumpTime = jumpMaxTime;
        mainCam = Camera.main;
        playerInput = new PlayerInput();
        characterController= GetComponent<CharacterController>();
        boxCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");   //��Animator�Ĳ���������һ��ID
        isRunningHash = Animator.StringToHash("isRunning");
        initialization();
    }
    private void Start()
    {
        //damageZone =FindObjectOfType<>
        Cursor.visible = false;
    }
    void initialization()
    {
        playerInput.CharacterControl.Move.started += onMovementInput;  //���Ŀ�ʼ�߼�

        playerInput.CharacterControl.Move.canceled += onMovementInput;  //����ȡ���߼�
        playerInput.CharacterControl.Move.performed += onMovementInput;//����ִ���߼�
        playerInput.CharacterControl.Run.started += onRun;
        playerInput.CharacterControl.Run.canceled += onRun;
        playerInput.CharacterControl.Run.performed += onRun;
        playerInput.CharacterControl.Jump.started += onJump;
        playerInput.CharacterControl.Jump.performed += onJump;
        playerInput.CharacterControl.Jump.canceled += onJump;
        playerInput.CharacterControl.Aim.started += onAim;
        playerInput.CharacterControl.Aim.performed += onAim;
        playerInput.CharacterControl.Aim.canceled += onAim;
        playerInput.CharacterControl.Shoot.started += onShoot;
        playerInput.CharacterControl.Shoot.performed += onShoot;
        playerInput.CharacterControl.Shoot.canceled += onShoot;
    }
    void onMovementInput(InputAction.CallbackContext context)  //��ɫ��·����
    {
        //Vector3 camRight = new Vector3(mainCam.transform.right.x, 0f, mainCam.transform.right.z).normalized;
        //Vector3 camForward = new Vector3(mainCam.transform.forward.x, 0f, mainCam.transform.forward.z).normalized;
        // FinalMovement = currentMovement.x * camRight + currentMovement.z * camForward;              //ʹ��ɫǰ����Ϊ�������ǰ����
        //FinalRunMovement = currentRunMovement.x * camRight + currentRunMovement.z * camForward; 
        currentMovementInput = context.ReadValue<Vector2>();
        currentMovement.x = currentMovementInput.x ;
        currentMovement.z = currentMovementInput.y;
        currentRunMovement.x = currentMovementInput.x * 1.5f;      //�ܲ��ٶ�Ϊ��·��2��
        currentRunMovement.z = currentMovementInput.y * 1.5f;
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;//������ֵ����Ϊ0ʱ��boolֵΪ��
    }
    void onRun(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }
    void onJump(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }
    void onAim(InputAction.CallbackContext context)
    {
        isAimPressed = context.ReadValueAsButton();
    }
    void onShoot(InputAction.CallbackContext context)
    {
        isShootPressed = context.ReadValueAsButton();
    }
    void handleRotation()
    {
        Vector3 positionToLookAt;

        positionToLookAt.x = FinalMovement.x;
        positionToLookAt.y = 0.0f;                          //ֻ���X,Z�����ı�����ɽ�ɫת��
        positionToLookAt.z = FinalMovement.z;

        Quaternion currentRotation = transform.rotation;
         if (isMovementPressed&&!Aiming)
        {
            Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
            transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame*Time.deltaTime);
        }       
    }
    void handleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);

        if (isMovementPressed && !isWalking && interation == false)
        {
            animator.SetBool("isWalking", true); //��ʼ������·����
        }
        else if (!isMovementPressed && isWalking && interation == false)
        {
            animator.SetBool("isWalking",false); //ֹͣ������·����
        }
        if (interation == true)
        {
            animator.SetBool("isWalking", false); //ֹͣ������·����
        }
        if ((isMovementPressed && isRunPressed)  && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        else if ((!isMovementPressed || !isRunPressed) && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }
    
    void handleGravity()//Ϊ��ɫʩ������
    {       
        
        if (characterController.isGrounded)
        {
            
            jumpTime = jumpMaxTime;
            fallenSpeed.y = 0;
            //FinalMovement.y = _groundedGravity;
            //FinalRunMovement.y = _groundedGravity;
            fallenSpeed.y += _groundedGravity*Time.deltaTime;
        }
       if(!characterController.isGrounded)
        {
            fallenSpeed.y += _gravity * Time.deltaTime;
           
            
            characterController.Move(fallenSpeed);
        }
    }
    private void Update()
    {
        Vector3 camRight = new Vector3(mainCam.transform.right.x, 0f, mainCam.transform.right.z).normalized;
        Vector3 camForward = new Vector3(mainCam.transform.forward.x, 0f, mainCam.transform.forward.z).normalized;
        FinalMovement = currentMovement.x * camRight + currentMovement.z * camForward;              //ʹ��ɫǰ����Ϊ�������ǰ����
        FinalRunMovement = currentRunMovement.x * camRight + currentRunMovement.z * camForward; 
        handleGravity();
        handleAnimation();
        handleRotation();
        Isgrounded();
        takeDamageAnimation();
        aimAnimation();
        //HandleJump();
        if (isMovementPressed && interation == false)
        {
            characterController.Move(FinalMovement * moveSpeed * Time.deltaTime);
        }
        if (isRunPressed && isMovementPressed && interation == false && Aiming == false)
        {
            characterController.Move(FinalRunMovement * moveSpeed * Time.deltaTime);
        }
    }
    private void FixedUpdate()
    {
      
        HandleJump();
    }
    private void OnEnable()
    {
        playerInput.CharacterControl.Enable();
    }
    private void OnDisable()
    {
        playerInput.CharacterControl.Disable();
    }

  
  
    void HandleJump()  //��Ծ����
    {
      
        if (isJumpPressed &&jumpTime>0 && interation == false&&Aiming==false)
        {
            animator.SetBool("isJumping", true);          
            fallenSpeed.y = jumpForce;
            characterController.Move(fallenSpeed * Time.deltaTime);
            jumpTime -= Time.deltaTime;
        }
        if (Isgrounded())
        {
            animator.SetBool("isJumping", false);
        }

    }
    
    private bool Isgrounded()
    {
        
        RaycastHit hit;
        //Physics.Raycast(boxCollider.bounds.center, Vector3.down,out hit, boxCollider.bounds.extents.y+0.5f);
        Physics.BoxCast(boxCollider.bounds.center,boxCollider.size/2, Vector3.down,out hit, Quaternion.identity,boxCollider.bounds.extents.y/2 );
        var raycastAll = Physics.OverlapBox(boxCollider.bounds.center, boxCollider.size / 2, Quaternion.identity,layerMask);
        if(hit.collider != null)
        {
            isGounded = true;
        }
        else
        {
            isGounded = false;
        }
        return isGounded;
    }

   
    private void takeDamageAnimation()
    {
        if (takeDamage)
        {
            animator.SetTrigger("isHurt");
        }
    }
    private void aimAnimation()
    {
        if (isAimPressed)
        {
            animator.SetBool("isAiming", true);
        }
        else
        {
            animator.SetBool("isAiming", false) ;
        }
    }
    public void aimMove()
    { 
        animator.SetFloat("MoveX",currentMovementInput.x);
        animator.SetFloat("MoveZ", currentMovementInput.y);
    }
}


