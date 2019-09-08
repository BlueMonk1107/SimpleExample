using UnityEngine;
using System.Collections;
using SocketIO;

public class Follower : ViewBase
{

    public Targeter targeter;

    public float scanFrequency = 0.5f;
    private float lastScanFrequency = 0;
    public float stopFollowDistance = 2f;
    private UnityEngine.AI.NavMeshAgent agent;
    // Use this for initialization
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        targeter = GetComponent<Targeter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReadyToScan() && !targeter.IsInRange(stopFollowDistance))
        {
            agent.SetDestination(targeter.target.position);
        }
    }

    private bool isReadyToScan()
    {
        return Time.time - lastScanFrequency > scanFrequency && targeter.target;
    }

      private void OnFollow(SocketIOEvent obj)
    {
        Debug.Log("follow request " + obj.data);
        var player = PlayerSpawner.Instance.GetPlayer(obj.data["id"].ToString());
		var targetTransform = PlayerSpawner.Instance.GetPlayer(obj.data["targetId"].ToString()).transform;
		var target = player.GetComponent<Targeter>();
		target.target = targetTransform;
    }

    protected override void AddEventListener()
    {
        NetworkMgr.Instance.AddListener(Keys.Follow,OnFollow);
    }

    protected override void RemoveEventListener()
    {
        NetworkMgr.Instance.RemoveListener(Keys.Follow,OnFollow);
    }
}
