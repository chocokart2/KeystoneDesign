using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemySkill : MonoBehaviour
{
    [SerializeField] private GameObject minion;
    float minionDistance = 1f;
    int spawnMax = 10;
    float spawnInterval = 0.1f;
    private float despawnDelay = 10f;
    Rigidbody myRigidbody;

    private float plus = 0.1f;

    private List<MinionController> minionList = new List<MinionController>();

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        InvokeRepeating("SummonMinion", 0f, spawnInterval);
        StartCoroutine(SurroundToTargetRepeatedly());
    }

    private IEnumerator SurroundToTargetRepeatedly()
    {
        while (true)
        {
            SurroundToTarget();
            yield return new WaitForSeconds(0.1f);
            minionDistance += plus;
            plus += 0.01f;
        }
    }

    private void SummonMinion()
    {
        if (minionList.Count >= spawnMax)
        {
            CancelInvoke("SummonMinion");
            return;
        }

        Vector3 position = new Vector3(Random.Range(-14f, 14f), 1, Random.Range(-14f, 14f));
        GameObject clone = Instantiate(minion, position, Quaternion.identity);
        minionList.Add(clone.GetComponent<MinionController>());

        // 프리팹을 생성한 후 일정 시간이 지나면 제거
        StartCoroutine(DestroyPrefabAfterDelay(clone));
    }

    private IEnumerator DestroyPrefabAfterDelay(GameObject prefab)
    {
        yield return new WaitForSeconds(despawnDelay);
        Destroy(prefab);
    }

    public void MoveToTarget()
    {
        for (int i = 0; i < minionList.Count; ++i)
        {
            minionList[i].MoveTo(transform.position);
        }
    }

    public void SurroundToTarget()
    {
        for (int i = 0; i < minionList.Count; ++i)
        {
            float angle = 360 * i / minionList.Count;
            angle = Mathf.PI * angle / 180;
            float x = transform.position.x + Mathf.Cos(angle) * minionDistance;
            float z = transform.position.z + Mathf.Sin(angle) * minionDistance;
            if (minionList[i] == null) continue;
            minionList[i]?.MoveTo(new Vector3(x, transform.position.y, z));
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < minionList.Count; ++i)
        {
            float angle = 360 * i / minionList.Count;
            angle = Mathf.PI * angle / 180;
            float x = transform.position.x + Mathf.Cos(angle) * minionDistance;
            float z = transform.position.z + Mathf.Sin(angle) * minionDistance;
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(new Vector3(x, transform.position.y, z), 0.5f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Equals("Player"))
        {
            Rigidbody playerRigidbody = collision.collider.GetComponent<Rigidbody>();
            myRigidbody.mass = Mathf.Max(
                myRigidbody.mass - playerRigidbody.velocity.magnitude,
                1.0f);
        }
    }
}
