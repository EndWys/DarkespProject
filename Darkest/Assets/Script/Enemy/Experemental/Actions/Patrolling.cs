using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class Patrolling
{
    public List<Vector3> patrul_points;

    int patrul_index = 0;

    public bool Patrul(NavMeshAgent agent, Transform transform)
    {
        NavMeshPath nav_path = new NavMeshPath();
        agent.SetDestination(patrul_points[patrul_index]);
        agent.CalculatePath(patrul_points[patrul_index], nav_path);
        if ((patrul_points[patrul_index] - transform.position).magnitude < 1.6 || nav_path.status == NavMeshPathStatus.PathInvalid)
        {
            Debug.Log(patrul_index);
            if (patrul_index == patrul_points.Count - 1) patrul_index = 0;
            else patrul_index++;
        }
        return true;
    }
}
