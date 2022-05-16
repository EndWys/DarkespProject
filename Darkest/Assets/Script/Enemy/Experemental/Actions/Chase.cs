using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EnemySystem;

[System.Serializable]
public class Chaese 
{
    float time_delay = 1f;
    bool attak;

    public void seePlayer(Transform player, Transform enemy, int task_i)
    {
        EnemyComponent This_enemy = enemy.GetComponent<EnemyComponent>();

        if (player==null)
            return;
        if (This_enemy == null)
            Debug.Log("Where is enemy?");

        if (This_enemy.E.alert < 100)
        {
            for (int i = 0; i < This_enemy.acts.Length; i++)
                if (This_enemy.acts[i] == EnemySystem.ActionList.Chaese)
                    This_enemy.tasks[i] = false;
            This_enemy.E.alert += Time.deltaTime * 10;
            Debug.Log(This_enemy.E.alert);
        }
        else
            for (int i = 0; i < This_enemy.acts.Length; i++)
                if (This_enemy.acts[i] == EnemySystem.ActionList.Chaese)
                {
                    This_enemy.tasks[i] = true;
                } 
    }

    public bool Сhase(NavMeshAgent agent, Transform transform, Transform player)
    {
        Debug.Log("chasing");
        if ((player.position - transform.position).magnitude > 0.8f)
        {
            agent.SetDestination(player.position);
            agent.speed = 3;
            return false;
        }
        else if (time_delay > 0)
        {
            attak = true;
            return true;
        }
        return false;
    }
}
