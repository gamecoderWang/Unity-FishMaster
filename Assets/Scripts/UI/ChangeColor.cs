using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler//, IPointerDownHandler
{
    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    GetComponentInChildren<Text>().color = Color.black;
    //}

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponentInChildren<Text>().color = Color.white;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponentInChildren<Text>().color = Color.black;
    }
}
