using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendMessageView : ViewBase {

    // Use this for initialization
    void Start () {
		string message = "";
		 InputField input = transform.Find("InputField").GetComponent<InputField>();
		 input.onEndEdit.AddListener((text) => message = text);
		 input.text = "";
		 transform.Find("Send").GetComponent<Button>().onClick.AddListener(()=>{
			JSONObject jsonObject = new JSONObject(JSONObject.Type.OBJECT);
			jsonObject.AddField("id", PlayerData.ID);
        	jsonObject.AddField("chatMessage", message);
			NetworkMgr.Instance.Emit(Keys.Chat,jsonObject);
			input.text = "";
		});
	}

	protected override void AddEventListener()
    {
    }

    protected override void RemoveEventListener()
    {
    }
}
