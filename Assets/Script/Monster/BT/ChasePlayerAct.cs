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
        _enemy.position += direction * _moveSpeed * Time.deltaTime;
        //방향 전환
        Vector3 scale = _enemy.localScale;
        if(direction.x > 0)
        {
            scale.x = -Mathf.Abs(scale.x);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x);
        }
        _enemy.localScale = scale;

        return NodeState.Running;
    }
}
