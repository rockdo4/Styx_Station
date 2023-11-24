using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            GameObject singleton = new GameObject();
            Instance = singleton.AddComponent<T>();
            singleton.name = "(singleton) " + typeof(T).ToString();
            DontDestroyOnLoad(singleton);
            Debug.Log("[Singleton] An instance of " + typeof(T) +
                " is needed in the scene, so '" + singleton +
                "' was created with DontDestroyOnLoad.");
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
