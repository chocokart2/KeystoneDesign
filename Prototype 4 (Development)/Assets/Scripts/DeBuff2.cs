using System.Collections;
using UnityEngine;

public class DeBuff2 : EffectBase
{
    //private float originalMass;

    private void Start()
    {
        //originalMass = GetComponent<Rigidbody>().mass;
    }

    //// 적의 Rigidbody 크기를 증가시키는 함수
    //public void IncreaseRigidbodySize()
    //{
    //    Rigidbody enemyRb = GetComponent<Rigidbody>();
    //    if (enemyRb != null)
    //    {
    //        enemyRb.mass *= 2;  // 예시로 질량을 2배로 증가
    //        StartCoroutine(ResetRigidbodySize(enemyRb, 7));  // 7초 후 원래 크기로 되돌리기
    //    }
    //}

    //// 일정 시간이 지난 후 Rigidbody 크기를 원래대로 되돌리는 코루틴
    //private IEnumerator ResetRigidbodySize(Rigidbody enemyRb, float delay)
    //{
    //    yield return new WaitForSeconds(delay);
    //    enemyRb.mass = originalMass;
    //}
}
