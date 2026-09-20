using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using EBAC.StateMachine;

public class Player : MonoBehaviour
{
    public Transform _Player;
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
        stateMachine.RegisterStates(States.WALK, new PlayerStates(_Player));
        stateMachine.RegisterStates(States.IDLE, new PlayerStates(_Player));
        stateMachine.RegisterStates(States.JUMP, new PlayerStates(_Player));
        stateMachine.SwitchState(States.WALK);
    }

}
