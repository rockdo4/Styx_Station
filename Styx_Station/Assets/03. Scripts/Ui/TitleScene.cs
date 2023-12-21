using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScene : MonoBehaviour
{
    public string GameScene;
    public void GoGameScene()
    {
        SceneManager.LoadScene(GameScene);
    }
}
