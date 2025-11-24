using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAct : ActionNode
{
    //대기상태라서 아무것도 안함
    Rigidbody2D _rigidbody;

    public IdleAct(Rigidbody2D rigid)
    {
        _rigidbody = rigid;
    }

    public override NodeState Tick()
    {
        _rigidbody.velocity = Vector2.zero;
        return NodeState.Running;
    }
}
