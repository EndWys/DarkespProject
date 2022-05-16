using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Сhandelier : MonoBehaviour
{

    public Collider[] hitColliders;
    bool is_fall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Player_Act()
    {
        is_fall = true;
        transform.parent.gameObject.AddComponent<Rigidbody>();
    }


    void NPC_Act(Transform enemy)
    {
        bool chasing = enemy.GetComponent<Enemy_move>().chasing;
        if(!chasing)
            if (is_fall)
                enemy.GetComponent<Enemy_move>().stopNdoing(2f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Collision")
        {
            Enemy_stat enemy = other.transform.GetComponent<Enemy_stat>();
            enemy.hp -= 100;
            enemy.animation.Play("FallBack_L");
        }
    }
}
