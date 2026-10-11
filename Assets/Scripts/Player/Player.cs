using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour//, IDamageable
{
    public List<Collider> colliders;
    public Animator animator;
    public CharacterController characterController;
    public GameObject Pivot;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float Gravity = -9.8f;
    public float jumpSpeed = 15f;

    public float vSpeed = 0f;

    public KeyCode jumpKeyCode = KeyCode.Space;

    [Header("Run Setup")]    
    public KeyCode keyRun = KeyCode.LeftShift;    
    public float speedRun = 1.5f;

    [Header("Flash")]
    public List<FlashColor> flashColors;

    [Header("Health")]
    public HealthBase healthBase;

    private void OnValidate()
    {
        if (healthBase == null)
        {
            healthBase = GetComponent<HealthBase>();
        }
    }

    private void Awake()
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
        
    }

    public void Damage(float damage, Vector3 dir)
    {
        //Damage(damage);
    }
    
    void Update()
    {
        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVectorVertical = transform.forward * inputAxisVertical * speed;   
        var inputAxisHorizontal = Input.GetAxis("Horizontal");
        var speedVectorHorizontal = transform.right * inputAxisHorizontal * speed;  

        var isWalking = inputAxisVertical != 0 || inputAxisHorizontal != 0;        
        if(isWalking)        
        {            
            if(Input.GetKey(keyRun))            
            {                
                speedVectorVertical *= speedRun;
                speedVectorHorizontal *= speedRun;                
                animator.speed = speedRun;            
            }            
            else            
            {                
                animator.speed = 1;            
            }        
        }
        Debug.Log(characterController.isGrounded);
        if(characterController.isGrounded)        
        {            
            vSpeed = 0;            
            if(Input.GetKeyDown(jumpKeyCode))            
            {                
                vSpeed = jumpSpeed;            
            }        
        }     
        
        vSpeed  -= Gravity * Time.deltaTime;        
        speedVectorVertical.y = vSpeed;        
        
        if (characterController.enabled)
        {
            characterController.Move(speedVectorVertical * Time.deltaTime);    
            characterController.Move(speedVectorHorizontal * Time.deltaTime);  

            if (inputAxisHorizontal != 0)        
            {            
                if (inputAxisHorizontal > 0)            
                {                
                    Pivot.transform.rotation = Quaternion.Euler(0, 90, 0);           
                }            
                else if (inputAxisHorizontal < 0)            
                {                
                    Pivot.transform.rotation = Quaternion.Euler(0, -90, 0);     
                }     
            }

            if (inputAxisVertical != 0)        
            {            
                if (inputAxisVertical > 0)            
                {                
                    Pivot.transform.rotation = Quaternion.Euler(0, 0, 0);           
                }            
                else if (inputAxisVertical < 0)            
                {                
                    Pivot.transform.rotation = Quaternion.Euler(0, 180, 0);     
                }     
            } 
        }

        animator.SetBool("Run", inputAxisVertical != 0 || inputAxisHorizontal != 0);
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
}
