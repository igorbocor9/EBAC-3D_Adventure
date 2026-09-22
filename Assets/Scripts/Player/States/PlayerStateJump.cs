using EBAC.StateMachine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStateJump : StateBase
{
    public override void OnStateEnter()
    {
        playerbase.rigidbody.linearVelocity = Vector3.up * playerbase.jumpForce;
    }
}
