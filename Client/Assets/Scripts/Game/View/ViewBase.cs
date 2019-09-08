using UnityEngine;

public abstract class ViewBase : MonoBehaviour {

    protected virtual void OnEnable() {
        AddEventListener();
    }

    protected virtual void OnDisable() {
        RemoveEventListener();
    }

    protected abstract void AddEventListener();
    protected abstract void RemoveEventListener();
}