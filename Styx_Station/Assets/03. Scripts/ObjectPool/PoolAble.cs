using UnityEngine.Pool;
using UnityEngine;

//������ƮǮ�� ����� ������Ʈ���� ���� ���
//poolable�� ��ӹ��� ��ũ��Ʈ�� ������ƮǮ�� ��� ������.
public class PoolAble : MonoBehaviour
{
    public IObjectPool<GameObject> ObjPool { get; set; }

    public void ReleaseObject()
    {
        ObjPool.Release(gameObject);
    }
}
