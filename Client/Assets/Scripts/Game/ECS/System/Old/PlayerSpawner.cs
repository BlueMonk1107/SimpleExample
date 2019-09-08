using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using SocketIO;

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

    private Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    public GameObject SpawnPlayer(string id)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefab/Player");
        var player = UnityEngine.Object.Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        InitPlayer(player, id);
        AddPlayer(id, player);
        return player;
    }

    private void InitPlayer(GameObject player, string id)
    {
        player.AddComponent<NetworkEntity>().id = id;
        player.AddComponent<Follower>();
        player.AddComponent<Targeter>();
        player.AddComponent<ClickToFollow>();
        player.AddComponent<Attacker>();
        player.AddComponent<Hittable>();
        player.AddComponent<Navigator>();
        player.AddComponent<PlayerView>().Init(id);
        player.AddComponent<UpdatePosition>();
    }

    public GameObject GetPlayer(string id)
    {
        return players[id];
    }

    public void AddPlayer(string id, GameObject player)
    {
        Debug.Log("addplayer id:" + id);
        players.Add(id, player);
    }
    public void RemovePlayer(string id)
    {
        var player = players[id];
        UnityEngine.Object.Destroy(player);
        players.Remove(id);
    }
}
