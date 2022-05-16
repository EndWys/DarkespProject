using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opend_cell : MonoBehaviour
{
    StatsNsetings player_stats;

    void Player_Act(Transform player)
    {
        player_stats = player.GetComponent<StatsNsetings>();
        player_stats.Hideing();
        player_stats.light_force = 0;
    }
    void NPC_Act(Transform enemy)
    {
        bool chasing = enemy.GetComponent<Enemy_move>().chasing;
        if (!chasing)
        {
            enemy.transform.position = transform.position + Vector3.back * 0.05f;
            enemy.GetComponent<Enemy_move>().stopNdoing(3f);
        }
    }
}
