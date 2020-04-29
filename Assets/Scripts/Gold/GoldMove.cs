using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMove : MonoBehaviour {

	private Transform targetTransform;
	public float v;

	// Use this for initialization
	void Start () {
		targetTransform = GameObject.FindGameObjectWithTag("GoldCollection").GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
		if (SettingUIController.Instance.IsGameOn)
		{
			transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, v * Time.deltaTime);
		}
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.CompareTag("GoldCollection"))
		{
			gameObject.SetActive(false);
		}
	}
}
