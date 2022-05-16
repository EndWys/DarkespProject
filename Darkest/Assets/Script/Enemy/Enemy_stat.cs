using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_stat : MonoBehaviour
{
    public Animator animation;
    Transform stats_bar;
    Enemy_Vission vission;
    public int hp = 100;
    public float alertness = 0, anxiety = 0, fear = 0;
    public float alerm_timer=0; 
    // Start is called before the first frame update
    void Start()
    {
        animation = GetComponentInChildren<Animator>();
        vission = gameObject.GetComponent<Enemy_Vission>();
        stats_bar = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (vission.visability > 0.5f && vission.see_you)
        {
            if(alertness <= 100)
                alertness += vission.visability*2;
            alerm_timer = 5;
            //Debug.Log("Alertness - " + alertness);
        }
        else if (alerm_timer > 0) alerm_timer -= Time.deltaTime;
        else if(alertness > 0)alertness -= 0.15f;

        stats_bar.GetChild(0).transform.localPosition = new Vector3(0, 1 + (alertness * 0.666f)/100, 0);

        if (alertness > 99)
            gameObject.SendMessage("Сhase", vission.player);
        else gameObject.SendMessage("stop_chase");

        if (hp <= 0)
            death();
    }

    void death()
    {
        hp = 0;
        transform.GetChild(0).SetParent(null);
        Destroy(gameObject);

        Debug.Log("Zdoh");
    }
}
