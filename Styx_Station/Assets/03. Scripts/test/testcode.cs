using UnityEngine;
using UnityEngine.SceneManagement;

public class testcode : MonoBehaviour
{
    static string save = string.Empty;
    static string load = string.Empty;
    int[] prevTime = new int[5];
    static float result = 0;
    static float maxResult = 86400; // 24�ð��� �ʷ� ���� ���� 
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SceneManager.LoadScene("YYL_0102");
        }

    }
}
