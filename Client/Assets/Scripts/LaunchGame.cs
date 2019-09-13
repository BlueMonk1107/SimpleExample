using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchGame : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		InitNetworkMgr();
		InitLoginView();
	}

	private void InitNetworkMgr()
	{
		GameObject mgr = new GameObject("NetworkMgr");
		mgr.AddComponent<NetworkMgr>();
		mgr.AddComponent<ConnetView>();
	}

	private void InitLoginView()
	{
		GameObject prefab = Resources.Load<GameObject>(Paths.LoginView);
		GameObject view = Instantiate(prefab, transform);
		view.AddComponent<LoginView>();
	}
}
