using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaWaveMove : MonoBehaviour {

    public Transform seaWaveStart;
    public Transform seaWaveEnd;
    private Transform childTransform;

    void OnEnable()
    {
        transform.position = seaWaveStart.position;
        childTransform = transform.GetChild((UIController.Instance.level / 25 > 3 ? 3 : UIController.Instance.level / 25) - 1);
        childTransform.gameObject.SetActive(true);
    }

    void Update()
    {
        if (SettingUIController.Instance.IsGameOn)
        {
            transform.position = Vector3.MoveTowards(transform.position, seaWaveEnd.position, 3 * Time.deltaTime);
            if (transform.position.x <= seaWaveEnd.position.x)
                gameObject.SetActive(false);
            if (childTransform.position.x <= 0)
            {
                int level = UIController.Instance.level / 25 > 3 ? 3 : UIController.Instance.level / 25;
                UIController.Instance.ChangeBackground(level);
                childTransform.gameObject.SetActive(false);
            }
        }
    }
}
