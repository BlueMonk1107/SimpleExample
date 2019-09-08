using SocketIO;
using UnityEngine;

public class Util
{
    public static Vector3 GetVectorFromJson(SocketIOEvent obj)
    {
        return new Vector3(obj.data["x"].n, 0, obj.data["y"].n);
    }

    public static JSONObject VectorToJson(Vector3 vector)
    {
        JSONObject jsonObject = new JSONObject(JSONObject.Type.OBJECT);
        jsonObject.AddField("x", vector.x);
        jsonObject.AddField("y", vector.z);
        return jsonObject;
    }

    public static JSONObject PlayerIdToJson(string id)
    {
        JSONObject jsonObject = new JSONObject(JSONObject.Type.OBJECT);
        jsonObject.AddField("targetId", id);
        return jsonObject;
    }
}