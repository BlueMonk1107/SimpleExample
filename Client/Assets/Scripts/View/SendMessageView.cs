using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendMessageView : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
		string message = "";
		InputField input = transform.Find("InputField").GetComponent<InputField>();
		input.onEndEdit.AddListener((text) => message = text);
		input.text = "";
		
		transform.Find("Send").GetComponent<Button>().onClick.AddListener(() =>
		{
			Debug.Log(PlayerData.ID);
			JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
			json.AddField("id",PlayerData.ID);
			json.AddField("message",message);
			NetworkMgr.Instance.Emit(Keys.Chat,json);
			input.text = "";
		});
	}
}
