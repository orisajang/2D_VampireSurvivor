using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedCondition : ConditionNode
{
    private Transform _enemy;
    private Transform _player;
    private float _detectionRange;

    public PlayerDetectedCondition(Transform enemy, Transform player, float detectRanage)
    {
        _enemy = enemy;
        _player = player;
        _detectionRange = detectRanage;
    }


    public override NodeState Tick()
    {
        float distance = Vector2.Distance(_enemy.position, _player.position);
        return distance <= _detectionRange ? NodeState.Success : NodeState.Failure;
    }
}
