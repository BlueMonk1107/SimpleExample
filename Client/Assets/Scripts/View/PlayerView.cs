using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour {

	// Use this for initialization
	public void Init (string id)
	{
		gameObject.AddComponent<Move>();
		gameObject.AddComponent<AniController>();
		Data data = gameObject.AddComponent<Data>();
		data.ID = id;
	}
}
