using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public float effectDuration = 15.0f; // 버프 효과 지속 시간
    public float safeDistance = 50.0f; // 플레이어와 적 사이의 최소 거리

    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void ActivateBuff()
    {
        StartCoroutine(BuffCountdownRoutine());
    }

    private IEnumerator BuffCountdownRoutine()
    {
        playerController.isBuffActive = true;
        yield return new WaitForSeconds(effectDuration);
        playerController.isBuffActive = false;
    }
}