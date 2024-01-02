using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleScene : MonoBehaviour
{
    public string GameScene;
    private float time;
    private float timerDuration =3f;
    private AsyncOperation asyncLoad;
    public GameObject loadingBar;
    private bool sceneLoad;
    private void Awake()
    {
        //UIManager.Instance.gameObject.SetActive(false);
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
    }
    private void Update()
    {
        if(sceneLoad)
        {

            time += Time.unscaledDeltaTime;

            if ((time >= timerDuration && asyncLoad.progress >= 0.9f) || asyncLoad.allowSceneActivation)
            {
                asyncLoad.allowSceneActivation = true; 
            }
        }
    }
    private IEnumerator LaodGameSceneAsync()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(GameScene);
        loadingBar.SetActive(true);
        Debug.Log(DateTime.Now.ToString("hh:mm::ss:ff"));
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
