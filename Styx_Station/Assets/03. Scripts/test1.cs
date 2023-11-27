using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    private void Awake()
    {
        // 현재 위치를 저장
        Vector2 currentPosition = transform.position;

        // 현재 방향 벡터 (오른쪽 방향)
        Vector2 currentDirection = Vector2.right;

        // 3도 위로 회전 (반시계 방향으로 회전)
        Quaternion rotatedUp = Quaternion.Euler(0, 0, 3); // 반시계 방향으로 회전

        // 3도 회전한 방향으로 이동 (길이가 10인 벡터)
        Vector2 newDirection = rotatedUp * currentDirection;

        // 길이가 10인 벡터를 현재 위치에 더하여 새로운 위치를 얻음
        Vector2 newPosition = currentPosition + newDirection.normalized * 10f;

        // 새로운 위치로 이동
        GameObject.Instantiate(gameObject, newPosition, Quaternion.identity);
        //transform.position = newPosition;
    }
}
