using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : IComponent
{
    public bool ValueChanged { get; set; }
    public string ID { get; set; }
    public Transform Player { get; set; }
    public Animator Animator { get; set; }
    public UnityEngine.AI.NavMeshAgent Agent { get; set; }
}

public class MoveComponent : IComponent
{
    public bool ValueChanged { get; set; }
    public string ID { get; set; }
    public Vector3 Target { get; set; }
}

public class AniComponent : IComponent
{
    public bool ValueChanged { get; set; }
    public bool Attack { get; set; }

}