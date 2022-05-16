using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsNsetings : MonoBehaviour
{
    public GameObject RestrtButton;
    //
    Animator anim;

    public int hp = 100;
    public float light_force;
    float forces = 0;
    public bool hided;
    // Start is called before the first frame update
    void Start()
    {
        anim = transform.GetComponent<Animator>();
        //StartCoroutine(calculat_light());
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Time.timeScale = 0;
            RestrtButton.SetActive(true);
            Debug.Log("YOU DIED");
        }
        else RestrtButton.SetActive(false);

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        light_force = forces;
        //StartCoroutine(calculat_light());
        //Debug.Log("next frame");
        forces = 0;
    }


    void Ilumination(float light)
    {
        if (light < 0)
            light = 0;
        forces += light;
        //StartCoroutine(calculat_light());
    }


    public void Hideing()
    {
        if (hided)
        {
            hided = false;
            anim.SetTrigger("Unhide");
        }
        else
        {
            hided = true; anim.ResetTrigger("Unhide");
            anim.Play("hiding");
        } 
    }

    //void take_a_hit()
    //{
    //    Debug.Log("Ой ударили!!!");
    //    hp -= 30;
    //}
}
