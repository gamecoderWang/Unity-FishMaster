using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneUI : MonoBehaviour {

    #region 单例
    private static MainSceneUI instance;

    public static MainSceneUI Instance
    {
        get
        {
            if (instance == null)
                return GameObject.FindGameObjectWithTag("UIManager").GetComponent<MainSceneUI>();
            return instance;
        }
    }
    #endregion

    public Canvas initialCanvas;          //初始画布
    public Canvas transitionLoadCanvas;   //转换场景时的画布
    public GameObject UIController;       //UI控制器

    [System.NonSerialized]
    public bool isNewGame;                //是否点击新开游戏

    public void ContinueGameButtonClick()
    {
        isNewGame = false;
        StartCoroutine(LoadNewScene());
    }

    public void StartNewGameButtonClick()
    {
        isNewGame = true;
        StartCoroutine(LoadNewScene());
    }

    public void QuitGameButtonClick()
    {
        Application.Quit();
    }

    IEnumerator LoadNewScene()
    {
        DontDestroyOnLoad(UIController);
        initialCanvas.gameObject.SetActive(false);
        transitionLoadCanvas.gameObject.SetActive(true);
        Slider loadingSlider = transitionLoadCanvas.GetComponentInChildren<Slider>();
        Text loadingText = transitionLoadCanvas.GetComponentInChildren<Text>();
        float displayProgress = 0;
        AsyncOperation op = SceneManager.LoadSceneAsync("Start");
        op.allowSceneActivation = false;
        int toProgress;
        while (op.progress < 0.9f)
        {
            toProgress = (int)(op.progress * 100);
            while (displayProgress <= toProgress)
            {
                ++displayProgress;
                loadingSlider.value = displayProgress / 100;
                loadingText.text = "正在加载  " + displayProgress + "%";
                yield return new WaitForEndOfFrame();
            }
        }
        toProgress = 100;
        while (displayProgress < toProgress)
        {
            ++displayProgress;
            loadingSlider.value = displayProgress / 100;
            loadingText.text = "正在加载  " + displayProgress + "%";
            yield return new WaitForEndOfFrame();
        }
        op.allowSceneActivation = true;
    }

}

