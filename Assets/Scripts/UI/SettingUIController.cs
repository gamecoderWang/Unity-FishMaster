using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingUIController : MonoBehaviour {

    private static SettingUIController instance;
    
    public static SettingUIController Instance
    {
        get
        {
            if (instance == null)
                return GameObject.FindGameObjectWithTag("UIController").GetComponent<SettingUIController>();
            return instance;
        }
    }

    public GameObject settingObject;
    public GameObject helpObject;
    public Image settingButton;
    public Image helpButton;
    public GameObject[] fishIntroductionPanels;
    private Text[] settingTexts;
    private Text[] helpTexts;
    private int currentPage = 0;

    public Text audioEffectText;
    public Text bgmAudioText;

    [System.NonSerialized]
    public bool IsGameOn = true;

    void Start()
    {
        settingTexts = settingObject.GetComponentsInChildren<Text>();
        helpTexts = helpObject.GetComponentsInChildren<Text>();
    }

    public void SaveGame()
    {
        AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.clickButtonAudio);
        GameController.Instance.SaveDataToJson();
    }

    public void ChangeAudioEffect()
    {

        if (AudioContoller.Instance.otherAudioSourse.mute)
        {
            AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.clickButtonAudio);
            AudioContoller.Instance.otherAudioSourse.mute = false;
            audioEffectText.text = "关闭音效";
        }
        else
        {
            AudioContoller.Instance.otherAudioSourse.mute = true;
            audioEffectText.text = "开启音效";
        }
    }

    public void ChangeBgmAudio()
    {
        AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.clickButtonAudio);
        if (AudioContoller.Instance.bgmAudioSourse.mute)
        {
            AudioContoller.Instance.bgmAudioSourse.mute = false;
            bgmAudioText.text = "关闭音乐";
        }
        else
        {
            AudioContoller.Instance.bgmAudioSourse.mute = true;
            bgmAudioText.text = "开启音乐";
        }
    }

    public void ChangeToMainScene()
    {
        AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.clickButtonAudio);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

    public void BackToGame(GameObject obj)
    {
        helpButton.raycastTarget = true;
        settingButton.raycastTarget = true;
        AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.clickButtonAudio);
        obj.SetActive(false);
        IsGameOn = true;
    }

    public void QuitGame()
    {
        StopAllCoroutines();
        Application.Quit();
    }

    public void OpenHelpOrSettingPanel(GameObject obj)
    {
        helpButton.raycastTarget = false;
        settingButton.raycastTarget = false;
        AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.clickButtonAudio);
        obj.SetActive(true);
        IsGameOn = false;
        if (obj.name == "SettingPanel")
            ResetTextColor(settingTexts);
        else
            ResetTextColor(helpTexts);
    }

    public void OpenIntroductionPanels(GameObject obj)
    {
        ResetTextColor(helpTexts);
        AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.clickButtonAudio);
        obj.SetActive(true);
        helpObject.SetActive(false);
        if (obj.name == "FishIntroduction")
        {
            obj.transform.GetChild(0).gameObject.SetActive(true);
            currentPage = 0;
        }    
    }

    public void CloseIntroductionPanel(GameObject obj)
    {
        if (obj.name == "FishIntroduction")
            obj.transform.GetChild(currentPage).gameObject.SetActive(false);
        AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.clickButtonAudio);
        obj.SetActive(false);
        helpObject.SetActive(true);
    }

    public void PageTurning(bool isLeft)
    {
        AudioContoller.Instance.PlayOtherAudio(AudioContoller.Instance.clickButtonAudio);
        if (isLeft)
        {
            if(currentPage > 0)
            {
                fishIntroductionPanels[currentPage].SetActive(false);
                currentPage--;
                fishIntroductionPanels[currentPage].SetActive(true);
            }
        }
        else
        {
            if(currentPage < 5)
            {
                fishIntroductionPanels[currentPage].SetActive(false);
                currentPage++;
                fishIntroductionPanels[currentPage].SetActive(true);
            }
        }
    }

    private void ResetTextColor(Text[] texts)
    {
        foreach (Text item in texts)
        {
            item.color = Color.black;
        }
    }

}
