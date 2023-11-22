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
        public int count; //�⺻ �뷮
        public int maxCount; //�ִ� �뷮
    }

    public static ObjectPoolManager instance;

    public bool IsReady { get; private set; } //������Ʈ Ǯ �Ŵ��� �غ� �Ϸ� ǥ��

    [SerializeField]
    private ObjectInfo[] objectInfos = null;

    private string objectName; // ��ųʸ��� key ��

    //������ƮǮ���� ������ ��ųʸ�
    private Dictionary<string, IObjectPool<GameObject>> objPoolDic = new Dictionary<string, IObjectPool<GameObject>>();

    //������Ʈ�� ���� ���� �Ҷ� ����� ��ųʸ�
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
                Debug.LogFormat("{0} ������ƮǮ�� ��ϵ��� ���� ������Ʈ�Դϴ�.", objectInfos[i].objectName);
                return;
            }
            goDic.Add(objectInfos[i].objectName, objectInfos[i].prefab);
            objPoolDic.Add(objectInfos[i].objectName, pool);

            //������Ʈ �̸� ����
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

    private GameObject CreatePooledItem() //����
    {
        GameObject poolGo = Instantiate(goDic[objectName]); //objectName�� �˸��� ������ ����ؼ� ����
        poolGo.GetComponent<PoolAble>().ObjPool = objPoolDic[objectName]; //poolable�� ������ Ǯ �Ҵ�
        return poolGo;
    }

    private void OnTakeFromPool(GameObject poolGo) //Ȱ��ȭ
    {
        poolGo.SetActive(true);
    }

    private void OnReturnedToPool(GameObject poolGo) //��Ȱ��ȭ
    {
        poolGo.SetActive(false);
    }

    private void OnDestroyPoolObject(GameObject poolGo) //����
    {
        Destroy(poolGo);
    }

    public GameObject GetGo(string goName)
    {
        objectName = goName;
        if(!goDic.ContainsKey(goName)) //���� ������Ʈ
        {
            Debug.LogFormat("{0} ������ƮǮ�� ��ϵ��� ���� ������Ʈ�Դϴ�.", goName);
            return null;
        }

        return objPoolDic[goName].Get();
    }
}
