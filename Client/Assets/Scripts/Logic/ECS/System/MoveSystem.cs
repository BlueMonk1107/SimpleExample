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
            && entity.GetComponent<MoveComponent>().ValueChanged;
    }

    protected override void Execute(IEntity entity)
    {
        var move = entity.GetComponent<MoveComponent>();
        var player = entity.GetComponent<PlayerComponent>();

        player.Behaviour = BehaviourEnum.MOVE;
        player.Player.position = move.StartPos;
        player.Agent.SetDestination(move.TargetPos);
        entity.GetComponent<MoveComponent>().ValueChanged = false;
    }
}
