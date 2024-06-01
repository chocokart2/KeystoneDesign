using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject debuffPrefab;
    public GameObject debuff2Prefab;
    public GameObject debuff3Prefab;
    public GameObject buffPrefab;
    public GameObject buff2Prefab;
    public int waveNumber = 1;  // 현재 웨이브 번호

    public int enemyCount;  // 현재 남아있는 적의 수

    private float spawnRange = 9;  // 스폰 위치 범위

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        // 첫 번째 웨이브 시작 시 적과 디버프 및 버프 스폰
        SpawnEnemyWave(waveNumber);
        SpawnDebuffWave(waveNumber);
        SpawnBuffWave(waveNumber); // 첫 번째 웨이브에 버프 스폰
    }

    // Update is called once per frame
    void Update()
    {
        // 현재 남아있는 적의 수를 계산
        enemyCount = FindObjectsOfType<Enemy>().Length;

        // 모든 적이 제거되면 다음 웨이브로 이동
        if (enemyCount == 0)
        {
            waveNumber++;
            RemoveAllDebuffsAndBuffs();
            playerController.DeactivateBuff2(); // 스테이지가 끝날 때 버프2 비활성화
            SpawnEnemyWave(waveNumber);
            SpawnDebuffWave(waveNumber);
            SpawnBuffWave(waveNumber); // 각 웨이브에 버프 스폰
        }
    }

    // 랜덤한 스폰 위치를 생성
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    // 지정된 수의 적을 스폰
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; ++i)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    // 지정된 수의 디버프를 스폰
    void SpawnDebuffWave(int debuffsToSpawn)
    {
        for (int i = 0; i < debuffsToSpawn; ++i)
        {
            SpawnRandomDebuff();
        }
    }

    // 랜덤하게 디버프를 스폰
    void SpawnRandomDebuff()
    {
        float randomValue = Random.value;

        if (randomValue < 0.33f)
        {
            Instantiate(debuffPrefab, GenerateSpawnPosition(), debuffPrefab.transform.rotation);
        }
        else if (randomValue < 0.66f)
        {
            Instantiate(debuff2Prefab, GenerateSpawnPosition(), debuff2Prefab.transform.rotation);
        }
        else
        {
            Instantiate(debuff3Prefab, GenerateSpawnPosition(), debuff3Prefab.transform.rotation);
        }
    }

    // 지정된 수의 버프를 스폰
    void SpawnBuffWave(int buffsToSpawn)
    {
        for (int i = 0; i < buffsToSpawn; ++i)
        {
            SpawnRandomBuff();
        }
    }

    // 랜덤하게 버프를 스폰
    void SpawnRandomBuff()
    {
        float randomValue = Random.value;

        if (randomValue < 0.95f)
        {
            Instantiate(buffPrefab, GenerateSpawnPosition(), buffPrefab.transform.rotation);
        }
        else
        {
            Instantiate(buff2Prefab, GenerateSpawnPosition(), buff2Prefab.transform.rotation);
        }
    }

    // 모든 디버프와 버프 오브젝트를 제거
    void RemoveAllDebuffsAndBuffs()
    {
        // DeBuff, DeBuff2, DeBuff3, Buff, Buff2 태그를 가진 모든 오브젝트를 찾아 제거
        GameObject[] debuffs = GameObject.FindGameObjectsWithTag("DeBuff");
        foreach (GameObject debuff in debuffs)
        {
            Destroy(debuff);
        }

        GameObject[] debuff2s = GameObject.FindGameObjectsWithTag("DeBuff2");
        foreach (GameObject debuff2 in debuff2s)
        {
            Destroy(debuff2);
        }

        GameObject[] debuff3s = GameObject.FindGameObjectsWithTag("DeBuff3");
        foreach (GameObject debuff3 in debuff3s)
        {
            Destroy(debuff3);
        }

        GameObject[] buffs = GameObject.FindGameObjectsWithTag("Buff");
        foreach (GameObject buff in buffs)
        {
            Destroy(buff);
        }

        GameObject[] buff2s = GameObject.FindGameObjectsWithTag("Buff2");
        foreach (GameObject buff2 in buff2s)
        {
            Destroy(buff2);
        }
    }
}
