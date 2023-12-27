using UnityEngine.Pool;
using UnityEngine;

//오브젝트풀을 사용할 오브젝트들이 전부 상속
//poolable을 상속받은 스크립트만 오브젝트풀에 등록 가능함.
public class PoolAble : MonoBehaviour
{
    public IObjectPool<GameObject> ObjPool { get; set; }

    public virtual void ReleaseObject()
    {
        if(gameObject.activeSelf)
        {
            ObjPool.Release(gameObject);
        }
    }
}
