using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using EnemySystem;

namespace EnemySystem {

    public enum ActionList : int
    {
        Patrol = 0, ExtinctTorch = 1, GoToBox = 2, Chaese = 3
    }

    //public enum VisionList : int
    //{
    //    SeeTorch = ActionList.ExtinctTorch + 100, SeeBox = ActionList.GoToBox + 100, SeePlayer = ActionList.Chaese + 100,
    //}
}

public class EnemyComponent : MonoBehaviour
{
    NavMeshAgent agent;
    Transform player;

    [Header("Stats")]
    public Enemy E;
    [Space(4)]

    [Header("Actions")]
    public ActNfeel actNfeel;

    public Vision vision;

    [Header("Enemy's tasks")]

    public bool[] tasks;

    public ActionList[] acts;

    //public VisionList[] visi;

    private void Awake()
    {
        vision.visible = LayerMask.GetMask("TransparentFX", "Default", "Object","Visible");

        agent = transform.GetComponent<NavMeshAgent>();

        player = GameObject.Find("Player Experement").transform;
    }

    void Controll()
    {
        int i = tasks.Length-1;
        while (i > 0)
            if (!tasks[i])
                i--;
            else
                break;
        CallActionMethhuds((int)acts[i]);
    }

    private void Update()
    {
        vision.Sight(transform.position);

        Controll();

        for (int i = 0; i < acts.Length; i++)
            SeeAll(i);

    }

    //void Action(int i)
    //{
    //    int action_num = (int)acts[i];
    //    CallActionMethhuds(action_num);
    //}

    public void CallActionMethhuds(int ActNumber)
    {
        switch (ActNumber)
        {
            case 0:
                actNfeel.patrol.Patrul(agent, transform);
                break;
            case 1:
                actNfeel.extinctTorch.GoToTorch(agent, transform);
                break;
            case 2:
                actNfeel.goToBox.GoBox(agent, transform);
                break;
            case 3:
                actNfeel.chaese.Сhase(agent, transform, player);
                break;
            default:
                break;
        }
    }

    public void SeeAll(int i)
    {
        Transform desired;
        switch ((int)acts[i])
        {
            case 1:
                desired = vision.findObject("Torch");
                if (vision.see(desired, transform, true, 5))
                    actNfeel.extinctTorch.seeTorch(desired, transform, (int)acts[i]);
                break;
            case 2:
                desired = vision.findObject("Box");
                if (vision.see(desired, transform, true, 5))
                    actNfeel.goToBox.seeBox(desired, transform, (int)acts[i]);
                break;
            case 3:
                desired = vision.findObject("Player");
                if (vision.see(desired, transform, true, 5))
                    actNfeel.chaese.seePlayer(desired, transform, (int)acts[i]);
                break;
            default:
                break;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Inter-active")
            InterActiv(other);
    }

    public void InterActiv(Collider col)
    {
        if (col != null)
            col.SendMessage("NPC_Act",this);
    }
}
