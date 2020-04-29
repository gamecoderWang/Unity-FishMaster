using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFish : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        coll.gameObject.SetActive(false);
    }
}
