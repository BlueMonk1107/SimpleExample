using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatItem : MonoBehaviour {

	public void Init(string id,string message,Action callBack)
	{
		RectTransform nameTrans = transform.Find("Name").GetComponent<RectTransform>();
		RectTransform timeTrans = transform.Find("Time").GetComponent<RectTransform>();
		RectTransform contentTrans = transform.Find("Content").GetComponent<RectTransform>();

		SetTextContent(nameTrans, id);
		SetTextContent(contentTrans, message);
		SetTextContent(timeTrans, DateTime.Now.ToString("HH:mm:ss"));

		SetHeight(nameTrans, contentTrans,callBack);
	}

	private void SetTextContent(Transform trans, string content)
	{
		trans.GetComponent<Text>().text = content;
	}

	private void SetHeight(RectTransform nameTrans,RectTransform content,Action callBack)
	{
		StartCoroutine(Wait(nameTrans, content,callBack));
	}

	private IEnumerator Wait(RectTransform nameTrans,RectTransform content,Action callBack)
	{
		yield return null;
		float height = 0;
		height += nameTrans.sizeDelta.y;
		height += content.sizeDelta.y;
		RectTransform self = GetComponent<RectTransform>();
		self.sizeDelta = new Vector2(self.sizeDelta.x,height);

		if (callBack != null)
			callBack();
	}
}
