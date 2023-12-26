using UnityEngine.Pool;
using UnityEngine;

//������ƮǮ�� ����� ������Ʈ���� ���� ���
//poolable�� ��ӹ��� ��ũ��Ʈ�� ������ƮǮ�� ��� ������.
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
