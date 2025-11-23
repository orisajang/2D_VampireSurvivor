using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    Player _player;
    public PlayerIdleState(Player ply)
    {
        _player = ply;
    }

    public void Enter()
    {
        
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        if(_player.isTakeDamage)
        {
            _player.isTakeDamage = false;
            _player.SetState(new PlayerStunState(_player));
        }
    }
}
