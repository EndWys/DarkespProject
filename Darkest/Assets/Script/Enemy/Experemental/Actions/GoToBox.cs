using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class GoToBox
{
    public Transform box;

    public void seeBox(Transform box, Transform enemy, int task_i)
    {
        Debug.Log("box seen");
        EnemyComponent This_enemy;
        Boxes boxes;
        enemy.TryGetComponent<EnemyComponent>(out This_enemy);
        box.TryGetComponent<Boxes>(out boxes);
        if (boxes == null)
            return;
        if (This_enemy == null)
            return;
        if (boxes.player_stats != null)
        {
            This_enemy.actNfeel.goToBox.box = box;
            This_enemy.tasks[task_i] = true;
        }
    }

    public bool GoBox(NavMeshAgent agent, Transform transform)
    {
        if (box == null)
            return false;

        Debug.Log("go to box");
        NavMeshPath nav_path = new NavMeshPath();
        agent.SetDestination(box.position);
        agent.CalculatePath(box.position, nav_path);
        if ((box.position - transform.position).magnitude < 1)
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
