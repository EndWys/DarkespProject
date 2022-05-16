using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fight : MonoBehaviour
{
    Animator animator;
    Enemy_stat stats;
    Enemy_Vission vission;
    Enemy_move move;

    public bool targen_in_range;

    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        stats = transform.GetComponent<Enemy_stat>();
        vission = transform.GetComponent<Enemy_Vission>();
        move = transform.GetComponent<Enemy_move>();
    }


    IEnumerator attack(Transform target)
    {
        while (targen_in_range)
        {
            yield return new WaitForFixedUpdate();
            if (move.in_fight || move.chasing)
            {
                animator.Play("Attack" + move.orientation);
                yield return new WaitForSeconds(0.8f);
                target.SendMessage("take_a_hit");
            }
            //Debug.Log("attack");
        }
    }

    void attacked_normal(object[] value)
    {
        int in_hand = (int)value[0];
        Transform player = (Transform)value[1];

        //move.in_fight = true;
        if (in_hand == 0)
            if (!vission.see_you)
            {
                stats.hp -= 50;
                stats.alertness = 100;
                Debug.Log("Attack - success");
            }
            else
            {
                stats.hp -= 20;
                stats.alerm_timer = 5f;
                stats.alertness = 100;
                player.SendMessage("take_a_hit");
                animator.Play("Contr-Attack" + move.orientation);
            }
        else if (!vission.see_you && stats.alertness < 80)
        {
            stats.hp -= 100;
            switch (in_hand)
            {
                case 1: animator.Play("Death-Sword_L"); break;
                case 2: animator.Play("Death-Rope_L"); break;
                case 3: animator.Play("Death-Dagger_L"); break;
            }
        }
            

        //when animation end - move.in_fight = false;
    }

    float skill_4_timer;
    void attaked_skill4()
    {
        skill_4_timer = 3;
        move.immobilized();
        StartCoroutine(skill_4_hands());
        animator.Play("Death-Skill4_L");
    }
    IEnumerator skill_4_hands()
    {
        yield return new WaitForFixedUpdate();
        if (skill_4_timer > 0)
        {
            skill_4_timer -= Time.deltaTime;
            StartCoroutine(skill_4_hands());
        }
        else
        {
            stats.hp -= 100;
            move.immobilized();
        }
    }
}
