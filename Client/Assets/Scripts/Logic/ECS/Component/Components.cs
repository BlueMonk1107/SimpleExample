using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerComponent : ComponentBase<PlayerComponent>
{
    public string ID { get; set; }
    public NavMeshAgent Agent { get; set; }
    public Transform Player { get; set; }
    public Animator Animator { get; set; }
    public BehaviourEnum Behaviour {get;set;}
}

public class MoveComponent : ComponentBase<MoveComponent>
{
    public string ID { get; set; }
    public Vector3 StartPos { get; set; }
    public Vector3 TargetPos { get; set; }
}