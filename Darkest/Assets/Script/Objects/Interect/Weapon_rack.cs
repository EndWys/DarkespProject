using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_rack : MonoBehaviour
{
    bool  rack_fallen = false;
    Collider[] hitColliders;
    // Start is called before the first frame update
    private void Update()
    {

    }

    void NPC_Act(Transform enemy)
    {
        bool chasing = enemy.GetComponent<Enemy_move>().chasing;
        if (rack_fallen&&!chasing)
        {
            enemy.GetComponent<Enemy_move>().stopNdoing(3f);
            rack_fallen = false;
        }
    }

    void Player_Act(Transform player)
    {
        player.GetComponent<fightNskills>().in_hand = 1;
        rack_fallen = true;
        hitColliders = Physics.OverlapSphere(transform.position, 20, LayerMask.GetMask("Default"), QueryTriggerInteraction.Collide);
        for(int i=0;i<hitColliders.Length ; i++)
            if (hitColliders[i].name == "Collision")
                hitColliders[i].transform.SendMessageUpwards("Strange_noise", transform.position);
    }
}
