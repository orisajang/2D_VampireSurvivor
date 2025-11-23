using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunState : IPlayerState
{
    //플레이어와 스턴 시간 정보
    Player _player;
    float _stunTime = 2.0f;
    float _currentTime;

    //이동 불가를 위해
    PlayerController _playerController;

    public PlayerStunState(Player ply)
    {
        _player = ply;
    }

    public void Enter()
    {
        _currentTime = _stunTime;
        _playerController = _player.GetComponent<PlayerController>();
        _playerController.isControllAble = false;
        Debug.Log("플레이어 스턴상태!!");
    }

    public void Exit()
    {
        
    }

    public void Update()
    {
        //몬스터의 공격을 맞으면? 스턴상태가 된다.
        _currentTime -= Time.deltaTime;
        if(_currentTime <=0)
        {
            //다시 Idle 상태로
            _playerController.isControllAble = true;
            _player.SetState(new PlayerIdleState(_player));
        }
    }
}
