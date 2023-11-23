using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CustomEditor(typeof(PlayerObject))]
public class PlayerScriptObjectEditor :Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerObject playerObject = (PlayerObject)target;

        bool check = playerObject.attackPower == 0 || playerObject.attackSpeed <= 0f || playerObject.playerHp == 0 || playerObject.playerAttackRange <= 0f;
        if (playerObject.playerCharacter == null)
        {
            EditorGUILayout.HelpBox("Error: Do not found playercharacter", MessageType.Error);
        }
        if (check)
        {
            EditorGUILayout.HelpBox("Error: player Stats is 0", MessageType.Error);
        }
        if(!check &&  playerObject.playerCharacter != null)
        {
            if (GUILayout.Button("Add Component"))
            {
                if (playerObject.playerCharacter != null)
                {
                    var playerControllerCheck = playerObject.playerCharacter.GetComponent<PlayerController>();
                    var playerAttributesCheck = playerObject.playerCharacter.GetComponent<PlayerAttributes>();
                    var playerResultPlayerStats = playerObject.playerCharacter.GetComponent<ResultPlayerStats>();
                    if (playerControllerCheck == null)
                    {
                        var player = playerObject.playerCharacter.AddComponent<PlayerController>();
                        player.layerMask = 1 << 16;
                        var ani = player.GetComponentInChildren<Animator>();
                        var find = GameObject.Find("Ui").GetComponent<PlayerUpgradeStats>();
                        //if(find !=null)
                        //{
                        //    if(find.playerAnimator != ani.runtimeAnimatorController)
                        //        ani.runtimeAnimatorController = find.playerAnimator;
                        //    else
                        //    {
                        //        Debug.Log("애니메이션 컨트롤러가 같음");
                        //    }
                        //}

                        Debug.Log("Done make by PlayerController");
                    }
                    else
                    {
                        Debug.Log(" PlayerController 중복생성중");
                    }
                    if (playerAttributesCheck == null)
                    {
                        var data = playerObject.playerCharacter.AddComponent<PlayerAttributes>();
                        data.attackPower = playerObject.attackPower;
                        data.attackSpeed = playerObject.attackSpeed;
                        data.hp = playerObject.playerHp;
                        data.playerAttackRange = playerObject.playerAttackRange;

                        Debug.Log("Done make by PlayerAttributes");
                    }
                    else
                    {
                        Debug.Log(" PlayerAttributes 중복생성중");
                    }
                    if (playerResultPlayerStats == null)
                    {
                        playerObject.playerCharacter.AddComponent<ResultPlayerStats>();

                        Debug.Log("Done make by ResultPlayerStats");
                    }
                    else
                    {
                        Debug.Log(" ResultPlayerStats 중복생성중");
                    }
                }
            }
        }
       
    }
}