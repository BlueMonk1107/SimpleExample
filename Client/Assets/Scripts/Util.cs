using System.Collections;
using System.Collections.Generic;
using LitJson;
using SocketIO;
using UnityEngine;

public class Util  {

    public static bool GetBoolFromJson(JSONObject json)
    {
        if (json.ToString() == "true")
        {
            return true;
        }
        else if (json.ToString()  == "false")
        {
            return false;
        }
        else
        {
           Debug.LogError("the json is not a bool");
           return false;
        }
    }

    public static JSONObject VectorToJson(Vector3 pos)
    {
        JSONObject json = new JSONObject(JSONObject.Type.OBJECT);
        json.AddField("x",pos.x);
        json.AddField("y",pos.y);
        json.AddField("z",pos.z);
        return json;
    }
    
    public static Vector3 JsonToVector(JSONObject json)
    {
        return new Vector3(json["x"].f,json["y"].f,json["z"].f);
    }

    public static string GetId(SocketIOEvent data)
    {
        JsonData json = JsonMapper.ToObject(data.data.ToString());
        return json["id"].ToString();
    }
}
