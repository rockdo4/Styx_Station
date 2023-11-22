using UnityEngine.Pool;
using UnityEngine;
using System.Collections.Generic;
using System;
using System.Xml.Linq;

public class ObjectPoolManager : MonoBehaviour
{
    [System.Serializable]
    private class ObjectInfo
    {
        public string objectName;
        public GameObject prefab;
        public int count; //기본 용량
        public int maxCount; //최대 용량
    }

    public static ObjectPoolManager instance;

    public bool IsReady { get; private set; } //오브젝트 풀 매니저 준비 완료 표시

    [SerializeField]
    private ObjectInfo[] objectInfos = null;

    private string objectName; // 딕셔너리의 key 값

    //오브젝트풀들을 관리할 딕셔너리
    private Dictionary<string, IObjectPool<GameObject>> objPoolDic = new Dictionary<string, IObjectPool<GameObject>>();

    //오브젝트를 새로 생성 할때 사용할 딕셔너리
    private Dictionary<string, GameObject> goDic = new Dictionary<string, GameObject>();

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        Init();
    }

    private void Init()
    {
        IsReady = false;
        for(int i =0;i<objectInfos.Length; i++)
        {
            IObjectPool<GameObject> pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                OnDestroyPoolObject, true, objectInfos[i].count, objectInfos[i].maxCount);

            if (goDic.ContainsKey(objectInfos[i].objectName))
            {
                Debug.LogFormat("{0} 오브젝트풀에 등록되지 않은 오브젝트입니다.", objectInfos[i].objectName);
                return;
            }
            goDic.Add(objectInfos[i].objectName, objectInfos[i].prefab);
            objPoolDic.Add(objectInfos[i].objectName, pool);

            //오브젝트 미리 생성
            for(int j = 0; j < objectInfos[i].count; j++)
            {
                objectName = objectInfos[i].objectName;
                PoolAble poolAbleGo = CreatePooledItem().GetComponent<PoolAble>();
                poolAbleGo.ObjPool.Release(poolAbleGo.gameObject);
            }

        }

        Debug.Log("Object Pool is Ready");
        IsReady = true;
    }

    private GameObject CreatePooledItem() //생성
    {
        GameObject poolGo = Instantiate(goDic[objectName]); //objectName에 알맞은 프리팹 사용해서 생성
        poolGo.GetComponent<PoolAble>().ObjPool = objPoolDic[objectName]; //poolable에 생성한 풀 할당
        return poolGo;
    }

    private void OnTakeFromPool(GameObject poolGo) //활성화
    {
        poolGo.SetActive(true);
    }

    private void OnReturnedToPool(GameObject poolGo) //비활성화
    {
        poolGo.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject poolGo) //삭제
    {
        Destroy(poolGo);
    }

    public GameObject GetGo(string goName)
    {
        objectName = goName;
        if(!goDic.ContainsKey(goName)) //없는 오브젝트
        {
            Debug.LogFormat("{0} 오브젝트풀에 등록되지 않은 오브젝트입니다.", goName);
            return null;
        }

        return objPoolDic[goName].Get();
    }
}
