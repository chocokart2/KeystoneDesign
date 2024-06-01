using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    public float chaseSpeed = 5.0f; // 추적 속도
    public float chaseRange = 10.0f; // 추적 범위
    public float pushForce = 55.0f; // 밀어내는 힘

    private Rigidbody enemyRb;
    private GameObject player;
    private DeBuff deBuff;  // DeBuff 스크립트 참조 변수
    private PlayerController playerController;  // PlayerController 스크립트 참조 변수

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        deBuff = GetComponent<DeBuff>();  // DeBuff 스크립트 참조
        playerController = player.GetComponent<PlayerController>();  // PlayerController 스크립트 참조
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // 플레이어와의 거리가 일정 범위 안에 있는 경우 추적 속도로 이동
        if (distanceToPlayer <= chaseRange)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * chaseSpeed);
        }
        else
        {
            // 플레이어를 향해 이동 (기본 속도)
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed);
        }
        
        // 플레이어의 버프 상태에 따른 밀어내기 로직
        if (playerController.isBuffActive)
        {
            if (distanceToPlayer < playerController.buff.safeDistance)
            {
                // 플레이어로부터 밀어내는 방향 벡터 계산
                Vector3 directionAwayFromPlayer = (transform.position - player.transform.position).normalized;
                // 적을 밀어내는 힘을 추가
                enemyRb.AddForce(directionAwayFromPlayer * pushForce, ForceMode.Impulse);
            }
            else
            {
                // 플레이어를 향해 이동
                Vector3 lookDirection = (player.transform.position - transform.position).normalized;
                enemyRb.AddForce(lookDirection * speed);
            }
        }
        else
        {
            // 플레이어를 향해 이동
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * speed);
        }
        
        // 적이 너무 아래로 떨어지면 파괴
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    // 플레이어와 충돌 시 처리
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            deBuff.HandleCollisionWithPlayer(collision, transform);
        }
    }
}
