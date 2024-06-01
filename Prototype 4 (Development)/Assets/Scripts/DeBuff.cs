using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeBuff : EffectBase
{
    private bool hasPowerup = false;
    private float powerupStrength = 15.0f;

    public bool HasPowerup => hasPowerup;  // 파워업 상태를 외부에서 접근할 수 있도록 프로퍼티 추가

    // 파워업을 활성화하는 함수
    public void ActivatePowerup()
    {
        hasPowerup = true;
        StartCoroutine(PowerupCountdownRoutine());
    }

    // 파워업 상태에서 플레이어와 충돌 시 처리하는 함수
    public void HandleCollisionWithPlayer(Collision collision, Transform enemyTransform)
    {
        Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        Vector3 awayFromEnemy = (collision.transform.position - enemyTransform.position).normalized;

        Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);

        playerRigidbody.AddForce(awayFromEnemy * powerupStrength, ForceMode.Impulse);
    }

    // 파워업 지속 시간을 관리하는 코루틴
    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
    }
}
