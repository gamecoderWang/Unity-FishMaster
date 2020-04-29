using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    private static UIController instance;

    public static UIController Instance
    {
        get
        {
            if (instance == null)
                return GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
            return instance;
        }
    }

    public Text levelText;
    public Text levelNameText;
    public Slider expSlider;
    public Text moneyText;
    public Text smallTimer;
    public Text bigTimer;
    public GameObject BigTimerObject;
    public GameObject RewardObject;
    public GameObject NotEnoughMoneyTip;
    public GameObject UpLevelEffect;
    public Text UpLevelNum;
    public Sprite[] Backgrounds;
    public Image background;
    private string[] nameStr = {"渔场新人","初来乍到","略有小成","驾轻就熟","融会贯通","出神入化","登峰造极","捕鱼达人","一代宗师","大神在此", "返璞归真"};

    private float smallTimeNum = 60;
    private float bigTimeNum = 180;
    [System.NonSerialized]
    public int moneyNum = 15000;
    [System.NonSerialized]
    public float exp = 0;
    [System.NonSerialized]
    public int level = 1;


    void Update()
    {
        if (SettingUIController.Instance.IsGameOn)
        {
            UpdateTimerUI();
            UpdateMoneyUI();
            UpdateExperienceUI();
        }
    }

    void UpdateTimerUI()
    {
        smallTimeNum -= Time.deltaTime;
        bigTimeNum -= Time.deltaTime;
        smallTimer.text = (int)(smallTimeNum / 10) + "  " + (int)(smallTimeNum % 10);
        bigTimer.text = (int)bigTimeNum + "s";
        if(smallTimeNum <= 0)
        {
            AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.getSmallRewardAudio);
            moneyNum += 50;
            smallTimeNum = 60;
        }
        if(bigTimeNum <= 0)
        {
            BigTimerObject.SetActive(false);
            RewardObject.SetActive(true);
        }
    }

    void UpdateMoneyUI()
    {
        moneyText.text = "$" + moneyNum.ToString();
    }

    void UpdateExperienceUI()
    {
        expSlider.value = exp / (level * 400);   //该升级的经验值三个地方都要改    1
        while(exp >= level * 400) //2
        {     
            UpdateLevelUI();
        }
    }

    void UpdateLevelUI()
    {
        AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.upLevelAudio);
        exp = exp - level * 400;           //3
        level++;
        levelText.text = level.ToString();
        UpLevelEffectStart(level);
        if(level % 10 == 0)
        {
            UpdateNameUI();
        }
        if(level % 25 == 0)
        {
            ChangeScene();
        }
    }

    public void UpdateNameUI()
    {
        int nameNum = level / 10 > 10 ? 10 : level / 10;
        levelNameText.text = nameStr[nameNum];
        switch(nameNum / 2)
        {
            case 0:
                levelNameText.color = Color.white;
                break;
            case 1:
                levelNameText.color = Color.green;
                break;
            case 2:
                levelNameText.color = Color.blue;
                break;
            case 3:
                levelNameText.color = new Color(178, 0, 255, 255);
                break;
            case 4:
                levelNameText.color = Color.black;
                break;
            case 5:
                levelNameText.color = Color.white;
                break;
            default:
                break;
        }
    }

    public bool DecreaseMoney(int Money)
    {
        if((moneyNum - Money) >= 0)
        {
            moneyNum -= Money;
            return true;
        }
        else
        {
            if(NotEnoughMoneyTip.activeSelf == false)
            {
                NotEnoughMoneyTip.SetActive(true);
                StartCoroutine(DestroyNoMoneyTip());
            }
            return false;
        }
    }

    public void ChangeBigRewardButton()
    {
        AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.getBigRewardAudio);
        moneyNum += 1000;
        RewardObject.SetActive(false);
        BigTimerObject.SetActive(true);
        bigTimeNum = 180;
        bigTimer.text = (int)bigTimeNum + "s";
    }

    private IEnumerator DestroyNoMoneyTip()
    {
        yield return new WaitForSeconds(1);
        NotEnoughMoneyTip.SetActive(false);
    }

    public void ChangeBackground(int BackgroundIndex)
    {
        AudioContoller.Instance.ChangeBgmAudio(AudioContoller.Instance.bgmAudios[BackgroundIndex]);
        background.sprite = Backgrounds[BackgroundIndex];
    }

    private void UpLevelEffectStart(int Level)
    {
        UpLevelEffect.SetActive(true);
        UpLevelNum.text = Level.ToString();
        EffectController.Instance.StarEffectHappen();
        Invoke("UpLevelEffectEnd", 1);
    }

    private void UpLevelEffectEnd()
    {
        UpLevelEffect.SetActive(false);
    }

    private void ChangeScene()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Fish");
        foreach (GameObject item in objs)
        {
            item.SetActive(false);
        }
        EffectController.Instance.StartSeaWaveEffect();
    }

}
