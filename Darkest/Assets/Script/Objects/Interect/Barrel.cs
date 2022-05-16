using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    StatsNsetings player_stats;
    // Start is called before the first frame update
    void Player_Act(Transform player)
    {
        player_stats = player.GetComponent<StatsNsetings>();
        player_stats.Hideing();
    }
    void NPC_Act(Transform enemy)
    {
        bool chasing = enemy.GetComponent<Enemy_move>().chasing;
        if (chasing)
            if (player_stats != null)
            {
                if(player_stats.hided);
                    player_stats.Hideing();
                player_stats.transform.position = transform.position + new Vector3(1, 0, 0);
                Destroy(gameObject, 0.2f);
            }
    }
}
