using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push_Barrels : MonoBehaviour
{
    Animator barrels;
    bool falled;
    Collider[] hitColliders;
    // Start is called before the first frame update
    private void Start()
    {
        //barrels = transform.GetComponent<Animator>();
    }

    void NPC_Act(Transform enemy)
    {
        Enemy_stat _enemy = enemy.GetComponent<Enemy_stat>();
        _enemy.hp -= 100;
        _enemy.animation.Play("FallBack_L");
    }

    void make_noice()
    {
        hitColliders = Physics.OverlapSphere(transform.position, 20, LayerMask.GetMask("Default"), QueryTriggerInteraction.Collide);
        for (int i = 0; i < hitColliders.Length; i++)
            if (hitColliders[i].name == "Collision")
                hitColliders[i].transform.SendMessageUpwards("Strange_noise", transform.position);
    }

    void Player_Act()
    {
        transform.position = new Vector3(-2.5f, -2,-0.5f);
        //transform.rotation = new Quaternion(40, 0, 0,0);
        make_noice();
        Destroy(gameObject, 1f);
    }
}
