using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandA : MonoBehaviour
{
    public GameObject islandA;
    public GameObject islandB;
    public float deletionInterval = 10f; // ���� ���� (��)
    public int repeatCount = 5; // �ݺ� Ƚ��
    public float deletionProbability = 0.1f; // ���� Ȯ�� (10%)

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
                // IslandA ����
                Destroy(islandA);

                // IslandB ���� (IslandA ��ġ��)
                Instantiate(islandB, islandA.transform.position, islandA.transform.rotation);
            }
        }
    }
}
