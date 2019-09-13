using System.Collections;
using System.Collections.Generic;
using LitJson;
using SocketIO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginView : ViewBase {

	// Use this for initialization
	void Start ()
	{
		string name = "";
		string password = "";
		transform.Find("Name/InputField").GetComponent<InputField>()
			.onEndEdit.AddListener((text) => { name = text; });
		transform.Find("PassWord/InputField").GetComponent<InputField>()
			.onEndEdit.AddListener((text) => { password = text; });
		
		transform.Find("Login").GetComponent<Button>().onClick.AddListener(()=>
		{
			JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
			json.AddField("id",name);
			json.AddField("password",password);
			
			NetworkMgr.Instance.Emit(Keys.Login,json);
		});
	}

	protected override void AddEventListener()
	{
		NetworkMgr.Instance.AddListener(Keys.Login,LoginResult);
	}

	protected override void RemoveEventListener()
	{
		NetworkMgr.Instance.RemoveListener(Keys.Login,LoginResult);
	}

	private void LoginResult(SocketIOEvent data)
	{
		if (Util.GetBoolFromJson(data.data["result"]))
		{
			PlayerData.ID = Util.GetId(data);
			Debug.Log("login success");
			SceneManager.LoadScene("Game");
		}
	}
}
