using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;

public class StartGameView : ViewBase {

	// Use this for initialization
	void Start ()
	{
		GameObject prefab = Resources.Load<GameObject>(Paths.ChatView);
		Instantiate(prefab, transform).AddComponent<ChatView>();
		NetworkMgr.Instance.Emit(Keys.InitGameComplete);
		
		Camera.main.gameObject.AddComponent<Click>();
	}

	protected override void AddEventListener()
	{
		NetworkMgr.Instance.AddListener(Keys.Spawn,SpawnPlayer);
	}

	protected override void RemoveEventListener()
	{
		NetworkMgr.Instance.RemoveListener(Keys.Spawn,SpawnPlayer);
	}

	private void SpawnPlayer(SocketIOEvent data)
	{
		string id = Util.GetId(data);
		var player = PlayerSpawner.Instance.SpawnPlayer(id);
		player.AddComponent<PlayerView>().Init(id);
	}
}
