using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFolllow : MonoBehaviour {

    public RectTransform canvasRect = null;
    public Camera cam;
    float z = 0;

    void Update()
    {
        if (SettingUIController.Instance.IsGameOn)
        {
            Vector3 position;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, Input.mousePosition, cam, out position);
            bool b = position.x < transform.position.x;
            if (b)
                z = Vector3.Angle(Vector3.up, position - transform.position);
            else
                z = -Vector3.Angle(Vector3.up, position - transform.position);
            if (Mathf.Abs(z) < 75)
                transform.rotation = Quaternion.Euler(0, 0, z);                    //此处用rotation和localrotation效果一样
            else
            {
                if (b)
                    transform.rotation = Quaternion.Euler(0, 0, 75);
                else
                    transform.rotation = Quaternion.Euler(0, 0, -75);
            }
        }
    }
}
