using UnityEngine;
using System.Collections;

public class Attacker : MonoBehaviour {

	public float attackDistance = 1;
	public float attackRate = 2;
	float lastAttackTime = 0;
	Targeter targeter;

	void Start () {
		targeter = GetComponent<Targeter> ();	
	}
	
	void Update () {
		if (!isReadyToAttack ())
			return;
		
		if (isTargetDead ()) {
			targeter.ResetTarget();
			return;
		}

		if (targeter.IsInRange (attackDistance)) {
			Debug.Log("attacking " + targeter.target.name);
			var targetId = targeter.target.GetComponent<NetworkEntity> ().id;
			Attack (targetId);
			lastAttackTime = Time.time;
		}
	}

	public static void Attack(string targetId)
	{
		Debug.Log("attacking player id " + Util.PlayerIdToJson(targetId));
		NetworkMgr.Instance.Emit("attack", Util.PlayerIdToJson(targetId));
	}

	bool isTargetDead ()
	{
		return targeter.target.GetComponent<Hittable> ().IsDead;
	}

	private bool isReadyToAttack()
	{
		return Time.time - lastAttackTime > attackRate && targeter.target;
	}
}
