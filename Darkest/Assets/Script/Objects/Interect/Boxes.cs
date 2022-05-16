using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
    public StatsNsetings player_stats;

    void Player_Act(Transform player)
    {
        player_stats = player.GetComponent<StatsNsetings>();
        player_stats.Hideing();
    }

    void NPC_Act(EnemyComponent enemy)
    {
        if (player_stats != null)
        {
            player_stats.hided = false;
            player_stats.transform.position = transform.position + new Vector3(1, 0, 0);
            for (int i = 0; i < enemy.acts.Length; i++)
                if (enemy.acts[i] == EnemySystem.ActionList.GoToBox)
                    enemy.tasks[i] = false;
            Destroy(this);
            Destroy(gameObject, 0.2f);
        }
    }
}
