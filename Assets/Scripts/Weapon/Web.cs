using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : MonoBehaviour {

    public int continueTime;
    public int damage;

    private void DestroyMyself()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        Invoke("DestroyMyself", 1);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.CompareTag("Fish"))
        {
            coll.gameObject.SendMessage("Damaged", damage,SendMessageOptions.DontRequireReceiver);
        }
    }
}
