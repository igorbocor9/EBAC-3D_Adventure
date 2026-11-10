using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Core.Singleton;
using Cloth;

public class Player : Singleton<Player>//, IDamageable
{
    public List<Collider> colliders;
    public Animator animator;
    public CharacterController characterController;
    public GameObject Pivot;

    public Transform cameraTransform;

    public float baseSpeed = 1f;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float Gravity = -9.8f;
    public float jumpSpeed = 15f;

    public float vSpeed = 0f;

    public Vector3 MoveDirection { get; private set; }

    public KeyCode jumpKeyCode = KeyCode.Space;

    [Header("Run Setup")]    
    public KeyCode keyRun = KeyCode.LeftShift;    
    public float speedRun = 1.5f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Header("Health")]
    public HealthBase healthBase;

    [SerializeField] private ClothChange _clothChanger;

    private void OnValidate()
    {
        if (healthBase == null)
        {
            healthBase = GetComponent<HealthBase>();
        }
    }

    private void Start()
    {
        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }

    private void OnKill(HealthBase h)
    {
        animator.SetTrigger("Death");
        colliders.ForEach(c => c.enabled = false);
        Invoke(nameof(Revive), 3f);
    }

    private void TurnOnColliders()
    {
        colliders.ForEach(c => c.enabled = true);
    }

    private void Revive()
    {
        animator.SetTrigger("Revive");
        Invoke(nameof(TurnOnColliders), .1f);
        healthBase.ResetLife();
        Respawn();
    }

    public void Damage(HealthBase h)
    {
        flashColors.ForEach(flashColor => flashColor.Flash());
        EffectsManager.Instance.ChangeVignette();
        ShakeCamera.Instance.Shake();
        
    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }
    
    void Update()
    {
        
        if (characterController.enabled)
        {
            Walk();
            Jump();


            characterController.Move(MoveDirection * speed * Time.deltaTime);
        }
    }

    private void Walk()
    {
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = forward * vertical + right * horizontal;

        moveDirection.Normalize();

        MoveDirection = moveDirection;

        if (moveDirection.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                10f * Time.deltaTime
            );
        }

        var isWalking = vertical != 0 || horizontal != 0;        
        if(isWalking)        
        {            
            if(Input.GetKeyDown(keyRun))            
            {                
                speed = speed * speedRun;            
                animator.speed = speedRun;            
            }            
            else if (Input.GetKeyUp(keyRun))    
            {                
                speed = baseSpeed;
                animator.speed = 1;            
            }        
        }
        animator.SetBool("Run", vertical != 0 || horizontal != 0);
    }

    private void Jump()
    {
        if(characterController.isGrounded)        
        {            
            vSpeed = 0;            
            if(Input.GetKeyDown(jumpKeyCode))            
            {                
                vSpeed = jumpSpeed;            
            }        
        }     
        
        vSpeed  -= Gravity * Time.deltaTime;  

        Vector3 gravForce = MoveDirection;
        gravForce.y = vSpeed;
        MoveDirection = gravForce;
    }

    [NaughtyAttributes.Button("respawn")]
    private void Respawn()
    {
        if (CheckpointManager.Instance.HasCheckpoint())
        {
            var lastCheckpointPosition = CheckpointManager.Instance.GetLastCheckpointPosition();
            lastCheckpointPosition.y += 1f;
            transform.position = lastCheckpointPosition;
        }
    }

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    private IEnumerator ChangeSpeedCoroutine(float speed, float duration)
    {
        var originalSpeed = this.speed;
        this.speed = this.speed*speed;
        yield return new WaitForSeconds(duration);
        this.speed = originalSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    private IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        _clothChanger.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _clothChanger.ResetTexture();
    } 
}
