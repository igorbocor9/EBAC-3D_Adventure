using EBAC.StateMachine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStateWalk : StateBase
{
    public override void OnStateStay()
    {
        playerbase.rigidbody.linearVelocity = new Vector3(playerbase._currentSpeed, playerbase.rigidbody.linearVelocity.y, playerbase.rigidbody.linearVelocity.z);
    }
}
