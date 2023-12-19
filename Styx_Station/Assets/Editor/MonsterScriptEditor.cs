//using Unity.VisualScripting;
//using UnityEditor;
//using UnityEngine;

//[CustomEditor(typeof(MonsterTypeBase))]
//public class MonsterScriptEditor : Editor
//{
//    public RuntimeAnimatorController controller;
//    public Canvas canvas;
//    public GameObject firepos;
//    public GameObject bPos;

//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();
//        MonsterTypeBase monster = (MonsterTypeBase)target;

//        if (monster.prefab != null)
//        {
//            if (GUILayout.Button("Add Component"))
//            {
//                var monsterStatsCheck = monster.prefab.GetComponent<MonsterStats>();
//                var MonsterControllerCheck = monster.prefab.GetComponent<MonsterController>();
//                var Rigidbody2DChack = monster.prefab.GetComponent<Rigidbody2D>();
//                var ColliderCheck = monster.prefab.GetComponent<CapsuleCollider2D>();
//                var monsterattackCheck = monster.prefab.GetComponent<MonsterAttackedTakeDamage>();
//                var MonsterAttackedRedEffectCheck = monster.prefab.GetComponent<MonsterAttackedRedEffect>();
//                var rec = monster.prefab.GetComponent<RectTransform>();
//                var child = monster.prefab.transform.GetChild(0);
//                var animator = child.GetComponent<Animator>();

//                //                //{
//                //                //    GameObject fpos = Instantiate(firepos);
//                //                //    fpos.transform.SetParent(monster.prefab.transform);
//                //                //    //fpos.transform.parent = monster.prefab.transform;

//                //                //    GameObject bpos = Instantiate(bPos);
//                //                //    bpos.transform.parent = monster.prefab.transform;

//                //                //    Canvas canvasObj = Instantiate(canvas);
//                //                //    canvasObj.transform.parent = monster.prefab.transform;
//                //}
//                {
//                    var ani = monster.prefab.GetComponentInChildren<Animator>();
//                    ani.runtimeAnimatorController = controller;
//                }
//                if (child != null && child.GetComponent<ExecuteHit>() == null)
//                {
//                    child.AddComponent<ExecuteHit>();
//                }
//                if (rec != null)
//                {
//                    rec.position = new Vector3(7.01f, -1.7f, 0f);
//                    rec.localScale = new Vector3(1f, 1f, 1f);
//                }
//                if (monsterStatsCheck == null)
//                {
//                    var mon = monster.prefab.AddComponent<MonsterStats>();
//                    mon.attackType = monster.attackType;
//                    Debug.Log("Set stats");
//                }
//                else
//                {
//                    Debug.Log(" MonsterStats 중복생성중");
//                }
//                if (MonsterControllerCheck == null)
//                {
//                    var mon = monster.prefab.AddComponent<MonsterController>();
//                    mon.startDelay = 1f;
//                    mon.range = 1f;
//                }
//                else
//                {
//                    Debug.Log(" MonsterController 중복생성중");
//                }

//                if (Rigidbody2DChack == null)
//                {
//                    var mon = monster.prefab.AddComponent<Rigidbody2D>();
//                    mon.gravityScale = 0f;
//                    mon.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
//                }
//                else
//                {
//                    Debug.Log(" Rigidbody2D 중복생성중");
//                }

//                if (ColliderCheck == null)
//                {
//                    var mon = monster.prefab.AddComponent<CapsuleCollider2D>();
//                    mon.size = new Vector2(0.62f, 0.99f);
//                    mon.offset = new Vector2(0f, 0.39f);
//                }
//                else
//                {
//                    Debug.Log(" CapsuleCollider2D 중복생성중");
//                }

//                if (monsterattackCheck == null)
//                {
//                    var mon = monster.prefab.AddComponent<MonsterAttackedTakeDamage>();
//                }
//                else
//                {
//                    Debug.Log(" AttackedTakeDamage 중복생성중");
//                }

//                if (MonsterAttackedRedEffectCheck == null)
//                {
//                    var mon = monster.prefab.AddComponent<MonsterAttackedRedEffect>();
//                    mon.redIntensity = 0.8f;
//                }
//                else
//                {
//                    Debug.Log(" MonsterAttackedRedEffect 중복생성중");
//                }
//            }
//        }
//    }
//}
