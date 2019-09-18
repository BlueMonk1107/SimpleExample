using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : ComponentBase<PlayerComponent>
{
    public string ID { get; set; }
    public Transform Player { get; set; }
    public Animator Animator { get; set; }
    public UnityEngine.AI.NavMeshAgent Agent { get; set; }
}

public class MoveComponent : ComponentBase<MoveComponent>
{
    public string ID { get; set; }
    public Vector3 Target { get; set; }
}

public class AniComponent : ComponentBase<AniComponent>
{
    public bool Attack { get; set; }

}