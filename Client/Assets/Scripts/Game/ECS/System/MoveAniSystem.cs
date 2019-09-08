using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAniSystem : LogicSystemBase {

	protected override Type[] Listener()
	{
		return new[] {typeof(PlayerComponent),typeof(AniComponent)};
	}

	protected override bool ExecuteCondition(IEntity entity)
	{
		return entity.GetComponent<PlayerComponent>().Animator != null
		       && entity.GetComponent<PlayerComponent>().Agent != null;
	}

	protected override void Execute(IEntity entity)
	{
		var animator = entity.GetComponent<PlayerComponent>().Animator;
		var agent = entity.GetComponent<PlayerComponent>().Agent;
		animator.SetFloat("Distance", agent.remainingDistance);
	}
}
