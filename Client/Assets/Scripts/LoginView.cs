using System.Collections;
using System.Collections.Generic;
using LitJson;
using SocketIO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginView : ViewBase
{
    // Use this for initialization
    private void Start()
    {
        string name = "";
        string password = "";
        transform.Find("Name/InputField").GetComponent<InputField>().onEndEdit.AddListener((text) => name = text);
        transform.Find("PassWord/InputField").GetComponent<InputField>().onEndEdit.AddListener((text) => password = text);
        transform.Find("Login").GetComponent<Button>().onClick.AddListener(()=>{
			JSONObject jsonObject = new JSONObject(JSONObject.Type.OBJECT);
        	jsonObject.AddField("id", name);
			jsonObject.AddField("loginPassword", password);
			NetworkMgr.Instance.Emit(Keys.Login,jsonObject);
		});
    }

	protected override void AddEventListener()
	{
		NetworkMgr.Instance.AddListener(Keys.Login,LoginSuccess);
	}

	protected override void RemoveEventListener()
	{
		NetworkMgr.Instance.RemoveListener(Keys.Login,LoginSuccess);
	}

	private void LoginSuccess(SocketIOEvent data)
	{
		 JsonData jsonData = JsonMapper.ToObject(data.data.ToString());
		 PlayerData.ID = jsonData["id"].ToString();
		Debug.Log(jsonData["chatMessage"]);
		SceneManager.LoadSceneAsync("Game");
	}

	

}
