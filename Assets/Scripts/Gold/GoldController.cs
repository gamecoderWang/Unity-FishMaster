using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldController : MonoBehaviour {

    #region 单例
    private static GoldController instance;

	public static GoldController Instance
	{
		get
		{
			if (instance == null)
				return GameObject.FindGameObjectWithTag("GoldController").GetComponent<GoldController>();
			return instance;
		}
	}
    #endregion

    public Transform goldHolder;
	public GameObject smallGold;
	private GameObject[] smallGoldPool = new GameObject[100];
	public GameObject bigGold;
	private GameObject[] bigGoldPool = new GameObject[100];

	// Use this for initialization
	void Start () {

		for (int i = 0; i < 100; i++)
		{
			GameObject obj = Instantiate(smallGold);
			obj.transform.SetParent(goldHolder,false);
			smallGoldPool[i] = obj;
			obj.SetActive(false);

			obj = Instantiate(bigGold);
			obj.transform.SetParent(goldHolder,false);
			bigGoldPool[i] = obj;
			obj.SetActive(false);
		}
	}

	public void ProduceGold(bool IsSmallGold, Transform GoldTransform)
	{
		if(IsSmallGold)
		{
			foreach (GameObject item in smallGoldPool)
			{
				if(item.activeSelf == false)
				{
					item.SetActive(true);
					item.transform.position = GoldTransform.position;
					break;
				}
			}
		}
		else
		{
			foreach (GameObject item in bigGoldPool)
			{
				if (item.activeSelf == false)
				{
					item.SetActive(true);
					item.transform.position = GoldTransform.position;
					break;
				}
			}
		}
	}
}
