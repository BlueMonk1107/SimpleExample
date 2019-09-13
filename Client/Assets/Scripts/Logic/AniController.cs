using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AniController : MonoBehaviour
{

	private Animator _animator;
	private NavMeshAgent _agent;
	private int _counter;

	// Use this for initialization
	void Start ()
	{
		_animator = GetComponent<Animator>();
		_agent = GetComponent<NavMeshAgent>();
		_counter = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (_counter > 10)
		{
			_animator.SetFloat("Distance",_agent.remainingDistance);
		}

		_counter++;
	}
}
