using UnityEngine;

public class ClickToMove : MonoBehaviour, IClickable
{
	void Start ()
	{
	}

    public void OnClick(RaycastHit hit)
    {
        //playerNavigator.NavigateTo(hit.point);
		//Network.Move(transform.position, hit.point);
        JSONObject jsonObject = new JSONObject(JSONObject.Type.OBJECT);
        jsonObject.AddField("id",PlayerData.ID);
        jsonObject.AddField("c", Util.VectorToJson(transform.position));
        jsonObject.AddField("d", Util.VectorToJson(hit.point));
        NetworkMgr.Instance.Emit(Keys.Move,jsonObject);
    }
}
