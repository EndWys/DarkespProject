using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_colls : MonoBehaviour
{
    Enemy_Fight fight;
    Enemy_move move;
    // Start is called before the first frame update
    private void Start()
    {
        fight = transform.GetComponentInParent<Enemy_Fight>();
        move = transform.GetComponentInParent<Enemy_move>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("trigger enter");
        if (other.tag == "Inter-activ")
        {
            InterActiv(other);
        }

        if (other.gameObject.name == "enemy_rng")
        {
            fight.targen_in_range = true;
            fight.StartCoroutine("attack", other.transform.parent);
            move.hit();
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "enemy_rng")
        {
            fight.targen_in_range = false;
            fight.StopCoroutine("attack");
            move.hit();
        }
    }

    public void InterActiv(Collider col)
    {
        if(col!=null)
            col.SendMessage("NPC_Act", transform.parent);
    }


}

