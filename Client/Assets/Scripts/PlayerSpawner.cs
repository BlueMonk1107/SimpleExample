using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner
{

	private static PlayerSpawner _instance;

	public static PlayerSpawner Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new PlayerSpawner();
			}

			return _instance;
		}
	}
	private Dictionary<string,GameObject> _playersDic = new Dictionary<string, GameObject>();

	public GameObject SpawnPlayer(string id)
	{
		GameObject prefab = Resources.Load<GameObject>(Paths.Player);
		GameObject player = Object.Instantiate(prefab);
		_playersDic.Add(id,player);
		return player;
	}

	public void RemovePlayer(string id)
	{
		var player = _playersDic[id];
		Object.Destroy(player);
		_playersDic.Remove(id);
	}

	public GameObject GetPlayer(string id)
	{
		return _playersDic[id];
	}
}
