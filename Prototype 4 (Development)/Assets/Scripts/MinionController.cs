using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveTo(Vector3 position)
    {
        if (navMeshAgent == null)
        {
            Debug.LogWarning($"����_BossEnemy : �׺�޽� ������Ʈ �ı��� ��ü��:{gameObject.name}");
            return;
        }


        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.SetDestination(position);
        }
        else
        {
            Debug.LogWarning($"�̰� ��Ƽ�� �ο��̺� �ȵ� {gameObject.name}");
        }
    }
}
