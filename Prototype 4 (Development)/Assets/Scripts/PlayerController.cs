using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EItemEffect
{
    none = 0,

    playerPowerup = 101,
    playerPushEnemy = 102,

    enemyPowerup = 201,
    enemyHeavy = 202,
    // 폭탄으로 인해 밀려나는것은 포함되지 않습니다.
}

public class PlayerController : MonoBehaviour
{
    private Coroutine currentCoroutine = null;
    private EItemEffect myEffect = EItemEffect.none;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerupStrength = 15.0f;


    private Vector3 respawnPoint = new Vector3(0, 10, 0);
    //private float playerDeathBottom = -10.0f;
    private int remainLife = 0;

    public GameObject powerupIndicator;
    public float speed = 5.0f;
    public bool hasPowerup = false;



    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");

        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);

        Vector3 piPosition = transform.position;
        piPosition.y = -0.5f;
        powerupIndicator.transform.position = piPosition;
    }
    
    public void AddLife()
    {
        remainLife++;
    }
    public void SetEffect(EItemEffect effect)
    {
        myEffect = effect;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.transform.position - transform.position).normalized;

            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerup);

            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        myEffect = EItemEffect.none;
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.SetActive(true);
        }
        if (other.CompareTag("DeathArea") && remainLife > 0)
        {
            transform.position = respawnPoint;
            playerRb.angularVelocity = Vector3.zero;
            playerRb.velocity = Vector3.zero;
            remainLife--;
        }
    }
    // 플레이어의 각종 정보를 저장하는 변수
    // 특정 이펙트를 담당하는 변수 + n초뒤 비활성화

}
