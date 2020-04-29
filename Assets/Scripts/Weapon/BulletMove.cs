using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
	Transform bulletTransform;
	public float velocity;

	//[System.NonSerialized]
	//public int damage;
	[System.NonSerialized]
	public int bulletIndex;

	void Start()
	{
		bulletTransform = GetComponent<Transform>();
	}

	// Update is called once per frame
	void Update()
	{
		if (SettingUIController.Instance.IsGameOn)
			bulletTransform.Translate(Vector3.up * velocity);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.CompareTag("Fish"))
		{
			WeaponController.Instance.ProduceWeb(this.bulletIndex, this.transform, WeaponController.Instance.bulletPower[bulletIndex]);
			gameObject.SetActive(false);
		}
	}
}
