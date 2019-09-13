using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0))
		{
			var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit))
			{
				HitGround(hit);
			}
		}
		
	}

	private void HitGround(RaycastHit hit)
	{
		JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
		json.AddField("id",PlayerData.ID);
		json.AddField("startPos",Util.VectorToJson(
			PlayerSpawner.Instance.GetPlayer(PlayerData.ID).transform.position));
		json.AddField("targetPos",Util.VectorToJson(hit.point));
		
		NetworkMgr.Instance.Emit(Keys.Move,json);
	}
}
