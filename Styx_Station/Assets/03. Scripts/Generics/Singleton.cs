using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    private static object _lock = new object();
    private static bool applicationIsQuitting = false;
    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (instance == null)
                {
                    instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        //Debug.LogError("[Singleton] Something went really wrong " +
                        //    " - there should never be more than 1 singleton!" +
                        //    " Reopening the scene might fix it.");
                        return instance;
                    }

                    if (instance == null)
                    {
                        GameObject singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);

                        //Debug.Log("[Singleton] An instance of " + typeof(T) +
                        //    " is needed in the scene, so '" + singleton +
                        //    "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        //Debug.Log("[Singleton] Using instance already created: " +
                        //    instance.gameObject.name);
                        DontDestroyOnLoad(instance.gameObject);
                    }
                }
                else if (FindObjectsOfType(typeof(T)).Length > 1)
                {
                    //T[] instances = FindObjectsOfType(typeof(T)) as T[];
                    //for (int i = 1; i < instances.Length; i++)
                    //{
                    //    Destroy(instances[i].gameObject);
                    //}
                }
                return instance;
            }
        }
    }
    public void OnApplicationQuit()
    {
        applicationIsQuitting = true;
    }
    public bool CheckInstance()
    {
        T[] instances = FindObjectsOfType(typeof(T)) as T[];
        if (instances.Length > 1)
        {
            Destroy(gameObject);
            return true;
        }
        else
            return false;
    }
}
