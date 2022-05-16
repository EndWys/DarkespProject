using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_animator : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    Animator[] anims;

    private void Start()
    {
        anims = transform.GetComponentsInChildren<Animator>();
    }

    //Skil 1
    void using_skill1_a(Vector3 target)
    {
        anims[0].transform.position = player.position;
        anims[0].transform.LookAt(target);
        anims[0].Play("Skil_1_animV2");
        anims[0].transform.GetChild(0).position = player.position;
        StartCoroutine(mask(1f));
        StartCoroutine(off_skill1_anim());
        Debug.Log("Mask_Cur_start");
    }
    IEnumerator mask(float timer)
    {
        yield return new WaitForFixedUpdate();
        anims[0].transform.GetChild(1).position = player.position;
        timer -= Time.deltaTime;
        if(timer > 0)
            StartCoroutine(mask(timer));
        //Debug.Log("Mask_Cur_start");
    }
    IEnumerator off_skill1_anim()
    {
        yield return new WaitForSeconds(1.2f);
        anims[0].transform.position = transform.position;
        anims[0].transform.GetChild(0).position = anims[0].transform.position;
        anims[0].transform.GetChild(1).position = anims[0].transform.position;
    }

    //Skil 2
    void using_skill2(Vector3 target)
    {
        anims[1].transform.position = target + Vector3.back*0.05f+Vector3.up*0.5f;
        anims[1].Play("Skill_2");
        StartCoroutine(off_skill2_anim());
    }
    IEnumerator off_skill2_anim()
    {
        yield return new WaitForSeconds(1.5f);
        anims[1].transform.position = transform.position;
    }

    //Skil 4
    void using_skill4(Vector3 target)
    {
        anims[3].transform.position = target + Vector3.back*0.5f + Vector3.up*0.5f;
        anims[3].Play("Skil4_anim");
        StartCoroutine(off_skill4_anim());
    }
    IEnumerator off_skill4_anim()
    {
        yield return new WaitForSeconds(3f);
        anims[1].transform.position = transform.position;
    }
}
