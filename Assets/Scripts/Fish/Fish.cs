using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {

    public int maxNum;
    public int speed;
    public int rotateSpeed = 0;
    public int maxBlood;
    public float exp;
    public int money;
    public int fishID;
    public bool isSmallGold;
    private int blood;

    public GameObject fishDie;

    public bool isMoveStraight;

    private Transform fishTr;

    void Awake()
    {
        fishTr = GetComponent<Transform>();
    }

    void OnEnable()
    {
        blood = maxBlood;
    }

    void Update()
    {
        if (SettingUIController.Instance.IsGameOn)
        {
            if (isMoveStraight)
                fishTr.Translate(Vector3.right * Time.deltaTime * speed);
            else
            {
                fishTr.Translate(Vector3.right * Time.deltaTime * speed);
                fishTr.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
            }
        }
    }

    public void Damaged(int damage)
    {
        this.blood -= damage;
        if(blood <= 0)
        {
            this.Die();
        }
    }

    private void Die()
    {
        AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.fishDieAudio);
        gameObject.SetActive(false);
        UIController.Instance.moneyNum += money;
        UIController.Instance.exp += exp;
        FishController.Instance.FishDie(fishID, transform);
        GoldController.Instance.ProduceGold(isSmallGold, transform);
    }

}
