using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public float effectDuration = 15.0f; // ���� ȿ�� ���� �ð�
    public float safeDistance = 50.0f; // �÷��̾�� �� ������ �ּ� �Ÿ�

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