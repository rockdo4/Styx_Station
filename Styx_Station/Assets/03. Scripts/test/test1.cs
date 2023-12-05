using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    private void Awake()
    {
        // ���� ��ġ�� ����
        Vector2 currentPosition = transform.position;

        // ���� ���� ���� (������ ����)
        Vector2 currentDirection = Vector2.right;

        // 3�� ���� ȸ�� (�ݽð� �������� ȸ��)
        Quaternion rotatedUp = Quaternion.Euler(0, 0, 3); // �ݽð� �������� ȸ��

        // 3�� ȸ���� �������� �̵� (���̰� 10�� ����)
        Vector2 newDirection = rotatedUp * currentDirection;

        // ���̰� 10�� ���͸� ���� ��ġ�� ���Ͽ� ���ο� ��ġ�� ����
        Vector2 newPosition = currentPosition + newDirection.normalized * 10f;

        // ���ο� ��ġ�� �̵�
        GameObject.Instantiate(gameObject, newPosition, Quaternion.identity);
        //transform.position = newPosition;
    }
}
