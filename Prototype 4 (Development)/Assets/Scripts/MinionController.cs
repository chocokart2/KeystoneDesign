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
            Debug.LogWarning($"오류_BossEnemy : 네비메쉬 에이전트 파괴됨 객체명:{gameObject.name}");
            return;
        }


        if (navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.SetDestination(position);
        }
        else
        {
            Debug.LogWarning($"이거 액티브 인에이블 안됨 {gameObject.name}");
        }
    }
}
