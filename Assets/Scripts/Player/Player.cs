using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EBAC.StateMachine;

public class Player : MonoBehaviour
{
    public Rigidbody rigidbody;

    public float _currentSpeed = 7f;
    public float jumpForce = 5f;

    public enum States
    {
        WALK,
        IDLE,
        JUMP
    }

    public StateMachine<States> stateMachine;

    private void Start()
    {
        stateMachine = new StateMachine<States>();
        stateMachine.Init();
        stateMachine.RegisterStates(States.WALK, new PlayerStateWalk());
        stateMachine.RegisterStates(States.IDLE, new PlayerStateIdle());
        stateMachine.RegisterStates(States.JUMP, new PlayerStateJump());
        stateMachine.SwitchState(States.IDLE);

    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            stateMachine.SwitchState(States.WALK);
        }
        else
        {
            stateMachine.SwitchState(States.IDLE);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.SwitchState(States.JUMP);
        }

        stateMachine.Update();
    }

}
