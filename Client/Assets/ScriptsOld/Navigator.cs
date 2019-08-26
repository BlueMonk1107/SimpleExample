using UnityEngine;
using System.Collections;

public class Navigator : MonoBehaviour {

    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;
	private Targeter targeter;
    
	void Awake () {
	    agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
	    animator = GetComponent<Animator>();
		targeter = GetComponent<Targeter>();

	}

    void Update()
    {
		animator.SetFloat("Distance", agent.remainingDistance);
    }
	
	public void NavigateTo (Vector3 position)
	{
	    agent.SetDestination(position);
		targeter.target = null;
		animator.SetBool ("Attack", false);
	}
}
