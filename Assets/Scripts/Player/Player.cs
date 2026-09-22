using UnityEngine;

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

    void Update()
    {
        transform.Rotate(0f, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0f);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;   

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
