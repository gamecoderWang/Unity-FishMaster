using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class GameController : MonoBehaviour {

    private static GameController instance;

    public static GameController Instance
    {
        get
        {
            if (instance == null)
                return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
            return instance;
        }
    }

    void Awake()
    {
        if (MainSceneUI.Instance.isNewGame)
            SetNewData();
        else
            GetJsonData();
        int sceneLevel = UIController.Instance.level / 25 > 3 ? 3 : UIController.Instance.level / 25;
        UIController.Instance.background.sprite = UIController.Instance.Backgrounds[sceneLevel];
        AudioContoller.Instance.ChangeBgmAudio(AudioContoller.Instance.bgmAudios[sceneLevel]);
        Destroy(MainSceneUI.Instance.gameObject);
    }

    public void GetJsonData()
    {
        string filePath = Application.streamingAssetsPath + "/content.json";
        string jsonText = File.ReadAllText(filePath);
        JsonData jsonData = JsonMapper.ToObject(jsonText);
        UIController.Instance.level = int.Parse(jsonData["level"].ToString());
        UIController.Instance.levelText.text = UIController.Instance.level.ToString();
        UIController.Instance.exp = float.Parse(jsonData["expPercentage"].ToString());
        UIController.Instance.UpdateNameUI();
        UIController.Instance.moneyNum = int.Parse(jsonData["money"].ToString());
    }

    public void SaveDataToJson()
    {
        JsonWriter jw = new JsonWriter();
        jw.WriteObjectStart();
        jw.WritePropertyName("level");
        jw.Write(UIController.Instance.level);
        jw.WritePropertyName("expPercentage");
        jw.Write(UIController.Instance.expSlider.value);
        jw.WritePropertyName("levelName");
        jw.Write(UIController.Instance.levelNameText.text);
        jw.WritePropertyName("money");
        jw.Write(UIController.Instance.moneyNum);
        jw.WriteObjectEnd();
        string jsonText = jw.ToString();
        string filePath = Application.streamingAssetsPath + "/content.json";
        File.WriteAllText(filePath, jsonText);
        //FileStream fs = new FileStream("Assets/Resources/content.json", FileMode.Create, FileAccess.ReadWrite);
        //StreamWriter sw = new StreamWriter(fs);
        //sw.WriteLine(jsonText);
        //sw.Close();
    }

    public void SetNewData()
    {
        FileStream fs = new FileStream(Application.streamingAssetsPath + "/content.json", FileMode.Create, FileAccess.ReadWrite);
        fs.Close();
    }
}
