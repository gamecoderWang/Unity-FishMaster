using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWaveEffect : MonoBehaviour {

	public Texture[] textures = null;
	private Material material = null;
	private int index = 0;
	// Use this for initialization
	void Start () {
		material = GetComponent<MeshRenderer>().material;
		InvokeRepeating("ChangeTexture", 0, 0.1f);
	}

	void ChangeTexture()
	{
		material.mainTexture = textures[index];
		index = (index + 1) % textures.Length;
	}
}
