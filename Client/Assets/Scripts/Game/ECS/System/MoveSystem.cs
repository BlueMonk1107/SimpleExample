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
        Vector3 position = entity.GetComponent<MoveComponent>().Target;
        Transform transform = entity.GetComponent<PlayerComponent>().Player;
        var _navigator = transform.GetComponent<Navigator>();
        _navigator.NavigateTo(position);
        entity.GetComponent<MoveComponent>().ValueChanged = false;
    }
}
