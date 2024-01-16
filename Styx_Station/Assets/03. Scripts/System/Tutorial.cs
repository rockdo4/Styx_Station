using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public List<TutorialIndex> index = new List<TutorialIndex>();

    [System.Serializable]
    public struct TutorialIndex
    {
        public string stringKey;
        public GameObject maskObj;
        public GameObject fingerObj;
    }
}
