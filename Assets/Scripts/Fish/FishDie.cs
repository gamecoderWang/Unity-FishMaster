using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDie : MonoBehaviour {

    void OnEnable()
    {
        float dieTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Invoke("DestroyMyself", dieTime);
    }

    void DestroyMyself()
    {
        gameObject.SetActive(false);
    }
}
