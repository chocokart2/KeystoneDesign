using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandA : MonoBehaviour
{
    public GameObject islandA;
    public GameObject islandB;
    public float deletionInterval = 10f; // 삭제 간격 (초)
    public int repeatCount = 5; // 반복 횟수
    public float deletionProbability = 0.1f; // 삭제 확률 (10%)

    void Start()
    {
        StartCoroutine(DeleteAndSpawnIsland());
    }

    IEnumerator DeleteAndSpawnIsland()
    {
        for (int i = 0; i < repeatCount; i++)
        {
            yield return new WaitForSeconds(deletionInterval);

            if (Random.value <= deletionProbability)
            {
                // IslandA 삭제
                Destroy(islandA);

                // IslandB 생성 (IslandA 위치에)
                Instantiate(islandB, islandA.transform.position, islandA.transform.rotation);
            }
        }
    }
}
