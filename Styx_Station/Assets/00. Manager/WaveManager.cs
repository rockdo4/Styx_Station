using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<WaveManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }

    private static WaveManager m_instance; // �̱����� �Ҵ�� static ����

    private void Awake()
    {
        // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ�
        if (instance != this)
        {
            // �ڽ��� �ı�
            Destroy(gameObject);
        }
    }
    public int CurrentStage { get; private set; }  //���� ��������
    public int CurrentWave { get; private set; } //���� ���̺�
    public int CurrentChpater { get; private set; } //���� é��
    public MonsterSpawner spawner;
    private int spawnMonsterCount = 10; //������ ���� ��

    public StageTable.StageTableData StageData { get; private set; }

    private void Start()
    {
        CurrentStage = 1;
        CurrentWave = 1;
        CurrentChpater = 1;

        var index = GameManager.instance.StageTable.GetIndex(CurrentChpater, CurrentStage, CurrentWave);

        StageData = GameManager.instance.StageTable.GetStageTableData(index);
    }

    public void StartWave()
    {
        spawner.SpawnMonster(spawnMonsterCount);
    }

    public void EndWave()
    {

    }

    public void UpdateCurrentChapter()
    {
        CurrentChpater++;
    }
    public void UpdateCurrentStage()
    {
        CurrentStage++;
    }
    public void UpdateCurrentWave()
    {
        CurrentWave++;
        if(CurrentWave > 5)
        {
            CurrentWave = 1;
        }
    }
}
