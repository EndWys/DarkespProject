using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class ExtinctTorch
{
    public Transform torch;

    public void seeTorch(Transform desired, Transform enemy, int n_task)
    {
        torch = desired;

        EnemyComponent This_enemy;
        Light light;
        enemy.TryGetComponent<EnemyComponent>(out This_enemy);

        if (This_enemy == null)
            Debug.Log("Enemy is null");

        torch.TryGetComponent<Light>(out light);

        if (light == null)
            Debug.Log("Torch is null");

        if (!light.enabled)
            This_enemy.tasks[n_task] = true;
    }

    public bool GoToTorch(NavMeshAgent agent, Transform transform)
    {
        NavMeshPath nav_path = new NavMeshPath();
        agent.SetDestination(torch.position);
        agent.CalculatePath(torch.position, nav_path);
        if ((torch.position - transform.position).magnitude < 1)
        {
            return false;
        }
        else if (nav_path.status == NavMeshPathStatus.PathInvalid)
        {
           return false;
        }
        return true;
    }
}
