using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSystem : LogicSystemBase {
    protected override Type[] Listener()
    {
        return new[] {typeof(PlayerComponent), typeof(MoveComponent)};
    }

    protected override bool ExecuteCondition(IEntity entity)
    {
        return entity.GetComponent<PlayerComponent>().ID == entity.GetComponent<MoveComponent>().ID
               && entity.GetComponent<MoveComponent>().ValueChanged();
    }

    protected override void Execute(IEntity entity)
    {
        Debug.Log("movesystem excute");
        Vector3 position = entity.GetComponent<MoveComponent>().Target;
        var agent = entity.GetComponent<PlayerComponent>().Agent;
        agent.SetDestination(position);
        AniComponent ani = entity.GetComponent<AniComponent>();
        ani.Attack = false;
    }
}
