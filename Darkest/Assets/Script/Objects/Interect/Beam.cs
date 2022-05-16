using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    bool is_fall;
    // Start is called before the first frame update
    void Player_Act()
    {
        
        transform.parent.gameObject.AddComponent<Rigidbody>();
        transform.parent.parent.GetChild(0).SendMessage("fall_whith");

    }
    void NPC_Act(Transform enemy)
    {
        bool chasing = enemy.GetComponent<Enemy_move>().chasing;
        if (chasing)
            if (is_fall)
                enemy.GetComponent<Enemy_move>().stopNdoing(2f);
    }
}
