using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSystem : LogicSystemBase {
    protected override Type[] Listener()
    {
        return new[] {typeof(PlayerComponent)};
    }

    protected override bool ExecuteCondition(IEntity entity)
    {
        return entity.GetComponent<PlayerComponent>().Behaviour != BehaviourEnum.IDLE
            && entity.GetComponent<PlayerComponent>().Agent != null;
    }

    protected override void Execute(IEntity entity)
    {
        var player = entity.GetComponent<PlayerComponent>();
        if ( player.Agent.remainingDistance >0 && player.Agent.remainingDistance < 1)
        {
            player.Behaviour = BehaviourEnum.IDLE;
            player.Animator.SetFloat("Distance",0);
        }
    }
}
