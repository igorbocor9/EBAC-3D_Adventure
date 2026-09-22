using EBAC.StateMachine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStateIdle : StateBase
{
    public override void OnStateEnter()
    {
        playerbase.rigidbody.linearVelocity = new Vector3(0, playerbase.rigidbody.linearVelocity.y, playerbase.rigidbody.linearVelocity.z);
    }
}
