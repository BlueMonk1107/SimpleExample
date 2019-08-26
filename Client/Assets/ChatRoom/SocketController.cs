using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using System;

using LitJson;

public class SocketController : MonoBehaviour
{


    SocketIOComponent socketIO;
    private bool login;
    private string chatMessage = "";
    private string chatContent = "";
    private string nickName = "";
    private Guid guid = Guid.NewGuid();


    // Use this for initialization
    void Start()
    {

        GameObject go = GameObject.Find("SocketIO");
        if (go == null)
        {
            go = new GameObject("SocketIO");
            socketIO = go.AddComponent<SocketIOComponent>();
        }
        else
        {
            socketIO = go.GetComponent<SocketIOComponent>();
            if (socketIO == null)
            {
                socketIO = go.AddComponent<SocketIOComponent>();
            }
        }

        //ws://127.0.0.1:3000/socket.io/?EIO=4&transport=websocket
        socketIO.url = "ws://127.0.0.1:3000/socket.io/?EIO=4&transport=websocket";

        socketIO.Connect();


        OnAddEvemt();
    }



    private void OnGUI()
    {

        if (!login)
        {
            Rect rect = new Rect(Screen.width / 3, Screen.height / 3, Screen.width / 3, Screen.height / 2);
            GUI.Box(rect, "");


            GUI.Label(
                new Rect(rect.x + Screen.width / 60,
                 rect.y + Screen.height / 9,
                 rect.width - Screen.width / 15,
                  Screen.height / 15)
                  , "请输入昵称：");

            nickName = GUI.TextField(
                new Rect(rect.x + Screen.width / 10,
                rect.y + Screen.height / 10,
                rect.width - Screen.width / 5,
                 Screen.height / 15)
                 , nickName);

            if (GUI.Button(new Rect(
                rect.x + Screen.width / 30,
                 rect.y + Screen.height / 4,
                  rect.width - Screen.width / 15,
                   Screen.height / 15)
                   , "登录"))
            {
                if (nickName.Length > 0)
                {
                    Dictionary<string, string> data = new Dictionary<string, string>();
                    data["guid"] = guid.ToString();
                    data["nickName"] = nickName;

                    socketIO.Emit(SocketIOProtocol.ProtocolLogin, new JSONObject(data));
                    Debug.Log("发送消息");
                }
            }

            return;
        }

        GUI.Label(new Rect(0, 0, Screen.width, Screen.height - 200), chatContent);

        chatMessage = GUI.TextField(new Rect(0, Screen.height - 200, Screen.width - 200, 200), chatMessage);
        if (GUI.Button(new Rect(Screen.width - 200, Screen.height - 200, 200, 200), "Send"))
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            data["chatMessage"] = chatMessage;
            socketIO.Emit(SocketIOProtocol.ProtocolChat, new JSONObject(data));

            chatMessage = string.Empty;
        }
    }




    void OnAddEvemt()
    {
        socketIO.On(SocketIOProtocol.ProtocolLogin, (date) =>
        {
            JsonData jsonData = JsonMapper.ToObject(date.data.ToString());
            string message = string.Format("{0} : {1}", jsonData["nickName"], jsonData["chatMessage"]);
            chatContent += message + "\r\n";
            login = true;
        });

        socketIO.On(SocketIOProtocol.ProtocolChat, (date) =>
        {
            JsonData jsonData = JsonMapper.ToObject(date.data.ToString());
            string message = string.Format("{0} : {1}", jsonData["nickName"], jsonData["chatMessage"]);
            chatContent += message + "\r\n";
        });

        socketIO.On(SocketIOProtocol.ProtocolInfo, (date) =>
        {
            Debug.Log("conected");
            Debug.Log(date.data);
        });
    }
}
