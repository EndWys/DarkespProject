using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tourch : MonoBehaviour
{
    Light tourch;
    // Start is called before the first frame update
    void Start()
    {
        tourch = GetComponentInParent<Light>();
    }

    // Update is called once per frame
    public void Player_Act()
    {
        transform.GetComponentInParent<Light>().enabled = false;
    }

    void NPC_Act(EnemyComponent enemy)
    {
        transform.GetComponentInParent<Light>().enabled = true;
        for(int i=0; i < enemy.acts.Length; i++)
            if(enemy.acts[i] == EnemySystem.ActionList.ExtinctTorch)
                enemy.tasks[i] = false;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.name == "Tourch_vission")
    //    {
    //        //Debug.Log("torch in radius");
    //        Enemy_move move = other.GetComponentInParent<Enemy_move>();
    //        if (!move.torchs.Contains(tourch)) { move.torchs.Add(tourch); }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.name == "Tourch_vission")
    //    {
    //        Enemy_move move = other.GetComponentInParent<Enemy_move>();
    //        if (move.torchs.Contains(tourch)) { move.torchs.Remove(tourch); }
    //    }
    //}

}
