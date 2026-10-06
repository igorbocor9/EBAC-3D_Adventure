using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float Gravity = -9.8f;
    public float jumpSpeed = 15f;

    public float vSpeed = 0f;

    public KeyCode jumpKeyCode = KeyCode.Space;

    [Header("Run Setup")]    
    public KeyCode keyRun = KeyCode.LeftShift;    
    public float speedRun = 1.5f;

    void Update()
    {
        transform.Rotate(0f, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0f);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;   

        var isWalking = inputAxisVertical != 0;        
        if(isWalking)        
        {            
            if(Input.GetKey(keyRun))            
            {                
                speedVector *= speedRun;                
                animator.speed = speedRun;            
            }            
            else            
            {                
                animator.speed = 1;            
            }        
        }

        if(characterController.isGrounded)        
        {            
            vSpeed = 0;            
            if(Input.GetKeyDown(jumpKeyCode))            
            {                
                vSpeed = jumpSpeed;            
            }        
        }     
        
        vSpeed  -= Gravity * Time.deltaTime;        
        speedVector.y = vSpeed;        
        
        characterController.Move(speedVector * Time.deltaTime);    

        animator.SetBool("Run", inputAxisVertical != 0);
    }
}
