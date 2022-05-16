using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lying_dagger : MonoBehaviour
{
    bool taken = false;
    // Start is called before the first frame update
    private void Update()
    {

    }

    void NPC_Act(Transform enemy)
    {

    }

    void Player_Act(Transform player)
    {
        player.GetComponent<fightNskills>().in_hand = 3;
        taken = true;
    }
}
