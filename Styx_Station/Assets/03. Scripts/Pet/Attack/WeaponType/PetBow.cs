using System;
using UnityEngine;
using UnityEngine.Rendering;

public class PetBow : PoolAble
{
    public event Action<GameObject, GameObject> OnCollided;
    private GameObject attacker;
    public float speed;
    public GameObject targetObject;
    private ContactFilter2D filter2D = new ContactFilter2D();
    public bool isRelease;
    private Rigidbody2D rb;

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");
        filter2D.SetLayerMask(layerMask);
    }
    private void FixedUpdate()
    {
        var vel = rb.velocity;
        Vector2 dir = (targetObject.transform.position-gameObject.transform.position ).normalized;
        var movePos = dir * speed * Time.deltaTime;
        vel += movePos;
        rb.velocity = vel;
    }
    private void OnEnable()
    {
        isRelease = false;
    }
    private void Update()
    {
        if (!IsInCameraView())
        {
            if (!isRelease)
            {
                isRelease = true;
                ReleaseObject();
            }
        }
        if(targetObject.GetComponent<MonsterStats>().currHealth <=0)
        {
            isRelease = true;
            ReleaseObject();
        }
    }
    public void SetPetBow(GameObject obj, float speed, GameObject targetObject)
    {
        attacker = obj;
        this.speed = speed;
        this.targetObject = targetObject;
    }

    public bool CheckOnCollided() 
    {
        return OnCollided != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collider2D[] colliders = new Collider2D[20];
        int count = Physics2D.OverlapCollider(gameObject.GetComponent<Collider2D>(), filter2D, colliders);
        //var test = Physics2D.OverlapCircle(transform.position, attacker.range, attacker.layerMask);
        Collider2D attackedMon = colliders[0];
        if (colliders.Length > 1)
        {
            for (int i = 0; i < count; i++)
            {
                if (colliders[i].GetComponent<MonsterStats>() == null)
                    continue;
                if (colliders[i].gameObject.GetComponentInChildren<SortingGroup>().sortingOrder >
                    attackedMon.GetComponentInChildren<SortingGroup>().sortingOrder &&
                    colliders[i].gameObject.GetComponent<MonsterStats>().currHealth > 0)
                {
                    attackedMon = colliders[i];
                }
            }
        }
        if (attackedMon == null)
        {
            string currentTime = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초");
            string log = $"{currentTime} : 데미지 받는 오브젝트가 Null임/ script : PetBow + 66번째줄";
            return;
        }
        if (attackedMon.GetComponent<MonsterStats>().currHealth <= 0)
        {
            string currentTime = DateTime.Now.ToString("MM월 dd일 HH시 mm분 ss초");
            string log = $"{currentTime} : 데미지 받는 몬스터가 HP0 이라 공격을 실패함/ script : PetBow + 73번째줄";
            return;
        }
        if (OnCollided != null)
        {
            if (attackedMon == null)
            {
                return;
            }
            OnCollided(attacker, attackedMon.gameObject);
        }
        if (!isRelease)
        {
            isRelease = true;
            ReleaseObject();
        }
    }
    private bool IsInCameraView()
    {
        var mainCamera = Camera.main;
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(transform.position);
        Vector3 screenPoint2D = mainCamera.ViewportToScreenPoint(screenPoint);
        return screenPoint2D.x > 0 && screenPoint2D.x < Screen.width && screenPoint2D.y > 0 && screenPoint2D.y < Screen.height && screenPoint.z > 0;
    }
}
