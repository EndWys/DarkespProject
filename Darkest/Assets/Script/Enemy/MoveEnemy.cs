using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveEnemy
{
    public bool[] tasks;

    public NavMeshAgent agent;

    Vector3 Movment(int i)
    {
        Vector3 place = new Vector3(0,0,0);
        switch (i)
        {
            case 5:
                break;
            default:
                break;
        }
        return place;
    }

    void Controll()
    {
        int i = tasks.Length;
        while (tasks[i] == true && i >= 0)
            i--;
        agent.Move(Movment(9));
    }


}
