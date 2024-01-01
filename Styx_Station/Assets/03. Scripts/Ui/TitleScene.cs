using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public string GameScene;
    private float time;
    public GameObject loadingBar;
    private void Awake()
    {
        //UIManager.Instance.gameObject.SetActive(false);
    }
    public void GoGameScene()
    {
        //Debug.Log(DateTime.Now.ToString("hh:mm::ss:ff"));
        //SceneManager.LoadScene(GameScene);
        StartCoroutine(LaodGameSceneAsync());
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
