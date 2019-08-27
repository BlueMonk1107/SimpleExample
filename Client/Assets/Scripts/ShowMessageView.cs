using System.Collections;
using System.Collections.Generic;
using LitJson;
using SocketIO;
using UnityEngine;
using UnityEngine.UI;

public class ShowMessageView : ViewBase
{
    private Transform _content;
    // Use this for initialization
    void Start()
    {
        _content = transform.Find("Viewport/Content");
    }

    protected override void AddEventListener()
    {
        NetworkMgr.Instance.AddListener(Keys.ReceiveChat, ShowMessage);
    }

    protected override void RemoveEventListener()
    {
        NetworkMgr.Instance.RemoveListener(Keys.ReceiveChat, ShowMessage);
    }

    private void ShowMessage(SocketIOEvent data)
    {
        JsonData jsonData = JsonMapper.ToObject(data.data.ToString());
        ChatItem item = SpawnItem();
        item.Init(jsonData["id"].ToString(), jsonData["chatMessage"].ToString(),UpdateContentPosition);
    }

    private void UpdateContentPosition()
    {
        Canvas.ForceUpdateCanvases();
        GetComponentInParent<ScrollRect>().verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }

    private ChatItem SpawnItem()
    {
        GameObject Prefab = Resources.Load<GameObject>("Prefab/ChatItem");
        return Instantiate(Prefab, _content).GetComponent<ChatItem>();
    }
}
