using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeBuff3 : MonoBehaviour
{
    public float pushStrength = 10.0f;  // 플레이어를 밀어내는 힘의 크기

    // 트리거 이벤트가 발생했을 때 호출되는 함수
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 awayFromObject = (other.transform.position - transform.position).normalized;
                playerRigidbody.AddForce(awayFromObject * pushStrength, ForceMode.Impulse);
                Debug.Log("Player pushed away from " + gameObject.name);
            }
        }
    }
}
