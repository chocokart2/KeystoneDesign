using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : EffectBase
{
    public float effectDuration = 15.0f; // ���� ȿ�� ���� �ð�
    public float safeDistance = 50.0f; // �÷��̾�� �� ������ �ּ� �Ÿ�

    //private PlayerController playerController;

    private void Awake()
    {
        endTime = effectDuration;
    }

    //private void Start()
    //{
    //    //playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    //}

    //public void ActivateBuff()
    //{
    //    StartCoroutine(BuffCountdownRoutine());
    //}

    //private IEnumerator BuffCountdownRoutine()
    //{
    //    playerController.isBuffActive = true;
    //    yield return new WaitForSeconds(effectDuration);
    //    playerController.isBuffActive = false;
    //}
}