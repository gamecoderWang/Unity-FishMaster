using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FishController : MonoBehaviour
{
    #region 单例
    private static FishController instance;

    public static FishController Instance
    {
        get
        {
            if (instance == null)
                return GameObject.FindGameObjectWithTag("FishController").GetComponent<FishController>();
            return instance;
        }
    }
    #endregion

    public Transform fishHolder;
    public Transform dieFishHolder;
    public Transform[] fishPositions;
    public GameObject[] fishes;
    private GameObject[][] fishPool;
    public GameObject[] dieFish;
    private GameObject[][] dieFishPool;

    private void Start()
    {
        InitializeFishes();
        StartCoroutine(ProduceFish());
    }

    private IEnumerator ProduceFish()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            if (SettingUIController.Instance.IsGameOn)
            {
                Random.InitState(Time.frameCount);
                int fishPosIndex = Random.Range(0, fishPositions.Length);
                int fishSpeciesIndex = Random.Range(0, fishes.Length);
                Fish fishPro = fishes[fishSpeciesIndex].GetComponent<Fish>();
                int num = Random.Range(fishPro.maxNum / 2 + 1, fishPro.maxNum);
                int moveType = Random.Range(0, 2);          //直走还是转弯，0直走，1转弯
                int angleOffset;                                        //直走偏转角，仅直走有效         
                int angleSpeed;                                       //转弯角速度，仅转弯有效

                if (moveType == 0)
                {
                    angleOffset = Random.Range(-30, 30);
                    StartCoroutine(ProduceStraightFish(fishPosIndex, fishSpeciesIndex, angleOffset, num));
                }
                else
                {
                    if (Random.Range(0, 2) == 0)
                        angleSpeed = Random.Range(9, 30);
                    else
                        angleSpeed = Random.Range(-9, -30);
                    StartCoroutine(ProduceTrunFish(fishPosIndex, fishSpeciesIndex, angleSpeed, num));
                }            
            }
            yield return new WaitForSeconds(0.8f);
        }
    }

    private IEnumerator ProduceStraightFish(int fishPosition, int fishSpecies, int angleoffset, int number)
    {
        int orderDecrease = 1;
        int fishNum = 1;
        foreach (GameObject item in fishPool[fishSpecies])
        {
            if (fishNum > number)
                break;
            if (item.activeSelf == false)
            {
                item.SetActive(true);
                fishNum++;
                Fish fish = item.GetComponent<Fish>();
                fish.isMoveStraight = true;
                item.transform.position = fishPositions[fishPosition].position;
                item.transform.rotation = fishPositions[fishPosition].rotation;
                item.GetComponent<SpriteRenderer>().sortingOrder += orderDecrease;
                orderDecrease++;
                item.transform.Rotate(Vector3.forward * angleoffset);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    private IEnumerator ProduceTrunFish(int fishPosition, int fishSpecies, int anglespeed, int number)
    {
        #region
        //for (int i = 1; i <= number; i++)
        //{
        //    GameObject fishObj = Instantiate(fishes[fishSpecies]);
        //    Fish fish = fishObj.GetComponent<Fish>();
        //    fish.isMoveStraight = false;
        //    fish.rotateSpeed = anglespeed;
        //    fishObj.transform.SetParent(fishHolder, false);           //试试如果没有加第二个参数会怎么样
        //    fishObj.transform.position = fishPositions[fishPosition].position;
        //    fishObj.transform.rotation = fishPositions[fishPosition].rotation;
        //    fishObj.GetComponent<SpriteRenderer>().sortingOrder += i;
        //    yield return new WaitForSeconds(0.5f);
        //}
        #endregion

        int orderDecrease = 1;
        int fishNum = 1;
        foreach (GameObject item in fishPool[fishSpecies])
        {
            if (fishNum > number)
                break;
            if(item.activeSelf == false)
            {
                item.SetActive(true);
                fishNum++;
                Fish fish = item.GetComponent<Fish>();
                fish.isMoveStraight = false;
                fish.rotateSpeed = anglespeed;
                item.transform.position = fishPositions[fishPosition].position;
                item.transform.rotation = fishPositions[fishPosition].rotation;
                item.GetComponent<SpriteRenderer>().sortingOrder += orderDecrease;
                orderDecrease++;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    private void InitializeFishes()
    {
        fishPool = new GameObject[18][];
        dieFishPool = new GameObject[18][];
        for (int i = 0; i < 18; i++)
        {
            Fish fish = fishes[i].GetComponent<Fish>();
            fishPool[i] = new GameObject[fish.maxNum * 3];
            dieFishPool[i] = new GameObject[fish.maxNum * 3];
            for (int t = 0; t < fish.maxNum * 3; t++)
            {
                GameObject fishObj = Instantiate(fishes[i]);
                fishPool[i][t] = fishObj;
                fishObj.transform.SetParent(fishHolder,false);
                fishObj.SetActive(false);

                GameObject dieFishObj = Instantiate(dieFish[i]);
                dieFishPool[i][t] = dieFishObj;
                dieFishObj.transform.SetParent(dieFishHolder,false);
                dieFishObj.SetActive(false);
            }
        }
    }

    public void FishDie(int FishID, Transform FishTransform)
    {
        if (FishID == 15)
            EffectController.Instance.GoldEffectHappen(FishTransform);
        foreach (GameObject item in dieFishPool[FishID])
        {
            if(item.activeSelf == false)
            {
                item.SetActive(true);
                item.transform.position = FishTransform.position;
                item.transform.rotation = FishTransform.rotation;
                break;
            }
        }
    }

}
