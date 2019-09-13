using System.Collections;
using System.Collections.Generic;
using LitJson;
using SocketIO;
using UnityEngine;
using UnityEngine.UI;

public class ShowMessageView : ViewBase
{
	private ScrollRect _scrollRect;
	private Transform _content;
	// Use this for initialization
	void Start ()
	{
		_scrollRect = GetComponentInChildren<ScrollRect>();
		_content = transform.Find("Viewport/Content");
	}

	protected override void AddEventListener()
	{
		NetworkMgr.Instance.AddListener(Keys.ReceiveChat,ShowChat);
	}

	protected override void RemoveEventListener()
	{
		NetworkMgr.Instance.RemoveListener(Keys.ReceiveChat,ShowChat);
	}

	private void ShowChat(SocketIOEvent data)
	{
		JsonData json = JsonMapper.ToObject(data.data.ToString());
		string id = json["id"].ToString();
		string message = json["message"].ToString();
		SpawnItem().Init(id,message,UpdateContentPos);
	}

	private ChatItem SpawnItem()
	{
		GameObject prefab = Resources.Load<GameObject>(Paths.ChatItem);
		GameObject item = Instantiate(prefab, _content);
		return item.AddComponent<ChatItem>();
	}

	private void UpdateContentPos()
	{
		Canvas.ForceUpdateCanvases();
		_scrollRect.verticalNormalizedPosition = 0;
	}
}
