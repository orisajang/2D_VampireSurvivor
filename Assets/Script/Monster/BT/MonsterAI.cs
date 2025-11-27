using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    private Transform _player;
    [SerializeField] private float _detectionRange = 5;
    [SerializeField] private float _moveSpeed = 2f;
    Rigidbody2D _rigidbody;

    private BTNode _root;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _player = PlayerManager.Instance._Player.transform;
        _root = new SelectorNode(new List<BTNode>
        {
            new SequenceNode(new List<BTNode>
            {
                new PlayerDetectedCondition(transform,_player,_detectionRange),
                new ChasePlayerAct(transform,_rigidbody,_player,_moveSpeed)
            }),
            new IdleAct(_rigidbody)
        });
    }
    private void Update()
    {
        _root.Tick();
    }

}
