using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePlayerAct : ConditionNode
{
    private Transform _enemy;
    private Transform _player;
    private float _moveSpeed;
    private Rigidbody2D _rigidbody;
    public ChasePlayerAct(Transform enemy,Rigidbody2D rigid, Transform player, float speed)
    {
        _enemy = enemy;
        _player = player;
        _moveSpeed = speed;
        _rigidbody = rigid;
    }

    public override NodeState Tick()
    {
        Vector3 direction = (_player.position - _enemy.position).normalized;
        _rigidbody.velocity = direction * _moveSpeed;
        //_enemy.position += direction * _moveSpeed * Time.deltaTime;
        return NodeState.Running;
    }
}
