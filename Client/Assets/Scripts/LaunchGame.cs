using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Spawn("Prefab/NetworkMgr");
		Spawn("Prefab/LoginView",transform);
		
	}

	private GameObject Spawn(string path,Transform parent = null)
	{
		GameObject temp = Resources.Load<GameObject>(path);
		return Instantiate(temp,parent);
	}
}
