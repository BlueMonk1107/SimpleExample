using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerComponent : IComponent
{
    public string ID { get; set; }
    public NavMeshAgent Agent { get; set; }
    public Transform Player { get; set; }
    public Animator Animator { get; set; }
    public bool ValueChanged { get; set; }
    public BehaviourEnum Behaviour {get;set;}
}

public class MoveComponent : IComponent
{
    public string ID { get; set; }
    public Vector3 StartPos { get; set; }
    public Vector3 TargetPos { get; set; }
    public bool ValueChanged { get; set; }
}