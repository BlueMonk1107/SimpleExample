using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAniSystem : LogicSystemBase {
    
    private int _counter;
    
    protected override Type[] Listener()
    {
        return new[] {typeof(PlayerComponent)};
    }

    protected override bool ExecuteCondition(IEntity entity)
    {
        var player = entity.GetComponent<PlayerComponent>();
        
        return player.Animator != null&& player.Agent != null && player.Behaviour == BehaviourEnum.MOVE;
    }

    protected override void Execute(IEntity entity)
    {
        if (_counter > 10)
        {
            var player = entity.GetComponent<PlayerComponent>();
            player.Animator.SetFloat("Distance",player.Agent.remainingDistance);
            _counter = 0;
        }
        else
        {
            _counter++;
        }
    }
}
