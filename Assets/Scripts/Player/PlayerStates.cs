using EBAC.StateMachine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStates : StateBase
{
    private Transform _player;
    private float _currentSpeed = 7;

    public PlayerStates(Transform player)
    {
        _player = player;
    }

    public override void OnStateStay()
    {
        _player.Translate(
            _player.forward * _currentSpeed * Time.deltaTime
        );
    }
}
