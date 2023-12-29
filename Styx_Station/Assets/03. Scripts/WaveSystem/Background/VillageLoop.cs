using UnityEngine;

public class VillageLoop : MonoBehaviour
{
    private float width;
    public Transform center;
    public Vector2 centerPos;
    private void Awake()
    {
        var boxCollider = GetComponent<BoxCollider2D>();
        width = boxCollider.size.x;// * 0.6f; ; // * 0.56f;
        centerPos = center.position;
    }

    void Update()
    {
        if (transform.position.x < centerPos.x - width) //화면 밖으로 나갔을 때
        {
            Reposition(); //다음 sky 배견화면 사진 옆으로 이동하게 함
        }
    }

    private void Reposition()
    {
        var offset = new Vector3(width * 2f, 0, 0);
        transform.position = transform.position + offset;
    }
}
