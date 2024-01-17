using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScene : MonoBehaviour
{
    public static TitleScene Instance;

    public string GameScene;
    private float time;
    private float timerDuration =10f;
    private AsyncOperation asyncLoad;
    public GameObject loadingBar;
    [HideInInspector] public  bool sceneLoad;
    public Slider loadingBarSlider;
    public Button button;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        //UIManager.Instance.gameObject.SetActive(false);
        if (!LabSystem.Instance.gameObject.activeSelf)
            LabSystem.Instance.gameObject.SetActive(true);
    }
    public void GoGameScene()
    {
        //Debug.Log(DateTime.Now.ToString("hh:mm::ss:ff"));
        //SceneManager.LoadScene(GameScene);
        //StartCoroutine(LaodGameSceneAsync());

        loadingBar.SetActive(true);
        asyncLoad = SceneManager.LoadSceneAsync(GameScene);
        asyncLoad.allowSceneActivation = false; 
        sceneLoad = true;
        button.interactable = false;
    }
    private void Update()
    {
        if(sceneLoad)
        {
            time += Time.unscaledDeltaTime;
            loadingBarSlider.value =time/timerDuration;
            if(!GameData.isLoad)
            {
                MakeTableData.Instance.gameSaveLoad.Load();
            }
            if ((time >= timerDuration ) || asyncLoad.allowSceneActivation)
            {
                loadingBarSlider.value = 1f;
                asyncLoad.allowSceneActivation = true; 
            }
        }
    }
    private IEnumerator LaodGameSceneAsync()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(GameScene);
        loadingBar.SetActive(true);
        while (!load.isDone)
        {
            time += Time.time;
            if (time > 10f)
            {
                load.allowSceneActivation = true;
            }
            yield return null;
        }
       
    }
}
