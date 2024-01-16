using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(SkillManager))] // YourScript는 실제 큐를 관리하는 스크립트로 변경해야 합니다.
//public class QueueInspector : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        base.OnInspectorGUI();

//        SkillManager script = (SkillManager)target; // YourScript는 실제 큐를 관리하는 스크립트로 변경해야 합니다.

//        GUILayout.Label("Queue Contents:");

//        foreach (var item in script.skillbutton)
//        {
//            EditorGUILayout.ObjectField(item, typeof(Object), true);
//        }
//    }
//}
