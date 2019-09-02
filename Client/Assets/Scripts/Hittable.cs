using UnityEngine;
using System.Collections;
using SocketIO;

public class Hittable : ViewBase {

	public float health = 100;
	Animator animator;
	public float respawnTime = 5;

    public bool IsDead {
		get { return health <= 0; }
	}

	void Start () {
		animator = GetComponent<Animator> ();
	}

	public void GetHit(float damage){
		health -= damage;
		if (IsDead) {
			animator.SetTrigger ("Dead");
			Invoke ("Spawn", respawnTime);
		}
	}

	void Spawn(){
		Debug.Log ("Spawn the player");
		transform.position = Vector3.zero;
		health = 100;
		animator.SetTrigger ("Spawn");
	}

	
	void OnAttack (SocketIOEvent obj)
	{
		Debug.Log("received attack " + obj.data);
		var targetPlayer = PlayerSpawner.Instance.GetPlayer(obj.data["targetId"].ToString());
		targetPlayer.GetComponent<Hittable> ().GetHit(20f);

		var attackingPlayer = PlayerSpawner.Instance.GetPlayer(obj.data["id"].ToString());
		attackingPlayer.GetComponent<Animator> ().SetTrigger ("Attack");

	}

    protected override void AddEventListener()
    {
       NetworkMgr.Instance.AddListener(Keys.Attack,OnAttack);
    }

    protected override void RemoveEventListener()
    {
        NetworkMgr.Instance.RemoveListener(Keys.Attack,OnAttack);
    }
}
