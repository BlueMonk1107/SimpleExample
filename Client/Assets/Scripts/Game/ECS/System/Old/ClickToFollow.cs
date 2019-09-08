using UnityEngine;

public class ClickToFollow : MonoBehaviour, IClickable
{
    private NetworkEntity networkEntity;
	private Targeter currentPlayerTargeter;
    void Start()
    {
        networkEntity = GetComponent<NetworkEntity>();
		currentPlayerTargeter = PlayerSpawner.Instance.GetPlayer(networkEntity.id).GetComponent<Targeter> ();
    }
    public void OnClick(RaycastHit hit)
    {
        Debug.Log("follow " + hit.collider.gameObject.name);
        Follow(networkEntity.id);
		currentPlayerTargeter.target = transform;
    }

    public void Follow(string id)
    {
        Debug.Log("send follow player id " + Util.PlayerIdToJson(id));
        NetworkMgr.Instance.Emit("follow", Util.PlayerIdToJson(id));
    }

}
