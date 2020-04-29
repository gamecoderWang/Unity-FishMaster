using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RewardTextAnimation : MonoBehaviour, IPointerEnterHandler{

	public Text text;
	private int fontSize = 30;
	private bool isLarger = true;

	public void OnPointerEnter(PointerEventData eventData)
	{
		StopCoroutine("ChangeScaleAnimation");
		text.fontSize = 30;
	}

	void OnEnable()
	{
		StartCoroutine("ChangeScaleAnimation");
	}

	IEnumerator ChangeScaleAnimation()
	{
		while (true)
		{
			if (isLarger)
			{
				if (fontSize <= 40)
				{
					text.fontSize = fontSize;
					fontSize++;
				}
				else
					isLarger = false;
			}
			else
			{
				if (fontSize >= 20)
				{
					text.fontSize = fontSize;
					fontSize--;
				}
				else
					isLarger = true;
			}
			yield return null;
		}
	}
}
