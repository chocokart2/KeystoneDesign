using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public float speed = 5.0f;
    public bool isBuffActive = false; // 버프 상태를 나타내는 변수
    public bool hasBuff2 = false; // 버프2 상태를 나타내는 변수

    public Buff buff; // Buff 스크립트 참조 변수
    public Buff2 buff2; // Buff2 스크립트 참조 변수

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");

        // Buff 및 Buff2 스크립트 초기화
        buff = GetComponent<Buff>();
        buff2 = GetComponent<Buff2>();
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        // 플레이어가 너무 아래로 떨어졌을 때
        if (transform.position.y < -10)
        {
            if (hasBuff2)
            {
                RespawnPlayer();
                hasBuff2 = false; // 한 번 리스폰되면 버프2 상태 해제
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    // 디버프 및 버프 아이템과 충돌 시 처리
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeBuff"))
        {
            Destroy(other.gameObject);
            IncreaseRigidbodySizeForEnemies();
        }
        else if (other.CompareTag("DeBuff2"))
        {
            Destroy(other.gameObject);
            IncreaseRigidbodySizeForEnemies();
        }
        else if (other.CompareTag("DeBuff3"))
        {
            Destroy(other.gameObject);
            PushPlayerAway(other.gameObject);
        }
        else if (other.CompareTag("Buff"))
        {
            Destroy(other.gameObject);
            ActivateBuff();
        }
        else if (other.CompareTag("Buff2"))
        {
            Destroy(other.gameObject);
            ActivateBuff2();
        }
    }

    // 주변의 모든 적의 Rigidbody 크기를 증가시키는 함수
    private void IncreaseRigidbodySizeForEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            DeBuff2 deBuff2 = enemy.GetComponent<DeBuff2>();
            if (deBuff2 != null)
            {
                deBuff2.IncreaseRigidbodySize();
            }
        }
    }

    // 플레이어를 밀어내는 함수
    private void PushPlayerAway(GameObject debuff3Object)
    {
        DeBuff3 deBuff3 = debuff3Object.GetComponent<DeBuff3>();
        if (deBuff3 != null)
        {
            Vector3 awayFromObject = (transform.position - debuff3Object.transform.position).normalized;
            playerRb.AddForce(awayFromObject * deBuff3.pushStrength, ForceMode.Impulse);
            Debug.Log("Player pushed away from " + debuff3Object.name);
        }
    }

    // 버프를 활성화하는 함수
    private void ActivateBuff()
    {
        if (buff != null)
        {
            buff.ActivateBuff();
        }
    }

    // 버프2를 활성화하는 함수
    private void ActivateBuff2()
    {
        if (buff2 != null)
        {
            buff2.ActivateBuff();
        }
    }

    // 버프2를 비활성화하는 함수
    public void DeactivateBuff2()
    {
        hasBuff2 = false;
    }

    // 플레이어를 랜덤 위치로 리스폰하는 함수
    private void RespawnPlayer()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-9, 9), 0, Random.Range(-9, 9));
        transform.position = spawnPosition;
        playerRb.velocity = Vector3.zero; // 리스폰 시 속도 초기화
        playerRb.angularVelocity = Vector3.zero; // 리스폰 시 회전 속도 초기화
    }
}
