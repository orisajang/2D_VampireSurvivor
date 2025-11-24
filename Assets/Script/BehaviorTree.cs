
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NodeState
{
    Running,
    Success,
    Failure
}

public abstract class BTNode
{
    public abstract NodeState Tick();

}

//액션 노드용 상속 클래스

public abstract class ActionNode : BTNode
{ //액션노드가 가지고있어야 할것이 생기면 추가
}

//컨디션 노드용 상속 클래스

public abstract class ConditionNode : BTNode
{
}

//시퀀스 노드가 사용할 클래스 하나라도 Success가 아니라면 즉시 반환

public class SequenceNode : BTNode
{
    private List<BTNode> _children = new List<BTNode>();
    public SequenceNode(List<BTNode> children)
    {
        _children = children;
    }
    public override NodeState Tick()
    {
        foreach (var child in _children)
        {

            //하나라도 Success가 아니라면 즉시 반환
            NodeState result = child.Tick();
            if (result != NodeState.Success) return result;
        }
        return NodeState.Success;
    }
}

//셀렉터 노드가 사용할 클래스 하나라도 Success나 Running이라면 상태 반환

public class SelectorNode : BTNode
{
    private List<BTNode> _children = new List<BTNode>();
    public SelectorNode(List<BTNode> children)
    {
        _children = children;
    }
    public override NodeState Tick()
    {
        foreach (var child in _children)
        {

            // 하나라도 Success나 Running이라면 상태 반환
            NodeState result = child.Tick();
            if (result != NodeState.Failure) return result;
        }
        return NodeState.Failure;
    }
}
