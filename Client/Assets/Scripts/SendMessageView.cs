using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendMessageView : ViewBase {

    // Use this for initialization
    void Start () {
		string message = "";
		 transform.Find("InputField").GetComponent<InputField>().onEndEdit.AddListener((text) => message = text);
		 transform.Find("Send").GetComponent<Button>().onClick.AddListener(()=>{
			JSONObject jsonObject = new JSONObject(JSONObject.Type.OBJECT);
			jsonObject.AddField("id", PlayerData.ID);
        	jsonObject.AddField("chatMessage", message);
			NetworkMgr.Instance.Emit(Keys.Chat,jsonObject);
		});
	}

	protected override void AddEventListener()
    {
    }

    protected override void RemoveEventListener()
    {
    }
}
