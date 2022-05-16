using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    bool rope_is = true;
    // Start is called before the first frame update
    void Player_Act(Transform player)
    {
        rope_is = false;
        player.GetComponent<fightNskills>().in_hand=2;

    }
    void NPC_Act(Transform enemy)
    {
        
    }
}
