using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAniSystem : LogicSystemBase {

	protected override Type[] Listener()
	{
		return new[] {typeof(PlayerComponent),typeof(AniComponent)};
	}

	protected override bool ExecuteCondition(IEntity entity)
	{
		return entity.GetComponent<PlayerComponent>().Animator != null
		       && entity.GetComponent<AniComponent>().ValueChanged();
	}

	protected override void Execute(IEntity entity)
	{
		var animator = entity.GetComponent<PlayerComponent>().Animator;
		var attack = entity.GetComponent<AniComponent>().Attack;
		animator.SetBool ("Attack", attack);
	}
}
