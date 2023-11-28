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
    public MonsterSpawner spawner;
    private int spawnMonsterCount = 10; //������ ���� ��
    void Start()
    {
        CurrentStage = 0;
        CurrentWave = 0;
    }

    public void StartWave()
    {
        spawner.SpawnMonster(spawnMonsterCount);
    }

    public void EndWave()
    {

    }
}
