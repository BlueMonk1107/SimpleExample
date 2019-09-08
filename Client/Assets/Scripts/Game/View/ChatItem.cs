using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatItem : MonoBehaviour
{
    // Use this for initialization
    public void Init(string id,string message,Action complete)
    {
        RectTransform name = transform.Find("Name").GetComponent<RectTransform>();
        RectTransform time = transform.Find("Time").GetComponent<RectTransform>();
        RectTransform content = transform.Find("Content").GetComponent<RectTransform>();
        SetName(name,id);
        SetTime(time);
        SetContent(content,message);
        SetHeight(name, content,complete);
    }

    private void SetName(RectTransform name,string id)
    {
        name.GetComponent<Text>().text = id;
    }

    private void SetTime(RectTransform time)
    {
        time.GetComponent<Text>().text = DateTime.Now.ToString("hh:mm:ss");
    }

    private void SetContent(RectTransform content,string message)
    {
        content.GetComponent<Text>().text = message;
    }

    private void SetHeight(RectTransform name, RectTransform content,Action complete)
    {
        StartCoroutine(Wait(name, content,complete));
    }

    private IEnumerator Wait(RectTransform name, RectTransform content,Action complete)
    {
		yield return null;
        float height = 0;
        height += name.sizeDelta.y;
        height += content.sizeDelta.y;
        RectTransform self = GetComponent<RectTransform>();
        self.sizeDelta = new Vector2(self.sizeDelta.x, height);

		if(complete != null)
		{
			complete();
		}
    }

    public float GetPreferredSize(RectTransform content)
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(content);
        ContentSizeFitter fitter = content.GetComponent<ContentSizeFitter>();
        return LayoutUtility.GetPreferredHeight(content);
    }
}
