using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatView : ViewBase {

	
    // Use this for initialization
    void Start () {
		transform.Find("ShowMessage").gameObject.AddComponent<ShowMessageView>();
		transform.Find("SendMessage").gameObject.AddComponent<SendMessageView>();
	}

    protected override void AddEventListener()
    {
    }

    protected override void RemoveEventListener()
    {
    }

}
