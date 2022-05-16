using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_move : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    Enemy_colls colls;
    Transform player_tr;
    public List<Light> torchs = new List<Light>();
    public List<Vector3> patrul_points = new List<Vector3>();
    int patrul_index = 0;
    float stop_timer;
    public bool chasing = false, staing = false, go_on_noise = false, in_fight = false, immobil=false;

    public string orientation = "_L";

    void Start()
    {
        animator = transform.GetComponentInChildren<Animator>();
        agent = GetComponentInParent<NavMeshAgent>();
        colls = GetComponentInChildren<Enemy_colls>();
    }

    // Update is called once per frame
    void Update() {
        if(!immobil)
            if(!in_fight)
                if (!staing)
                {
                    if (!go_on_noise)
                    {
                        if (!chasing)
                            if (!go_to_torch())
                                Patrul(patrul_index);
                    }
                    else if ((agent.pathEndPosition-transform.position).magnitude < 1.6)//agent.pathEndPosition.x == transform.position.x && agent.pathEndPosition.z == transform.position.z)
                        go_on_noise = false;
                }
                else
                {
                    if (stop_timer > 0)
                        stop_timer -= Time.deltaTime;
                    else staing = false;
                }

        if (agent.destination.x < transform.position.x)
        {
            orientation = "_L";
            animator.SetInteger("Orientation", 0);
        }
        else
        {
            orientation = "_R";
            animator.SetInteger("Orientation", 1);
        }
    }

    public void hit()
    {
        if (agent.speed == 0)
            agent.speed = 3;
        else agent.speed = 0;
    }

    public void immobilized()
    {
        Debug.Log("immobil");
        if (immobil)
            immobil = false;
        else
        {
            immobil = true;
            agent.SetDestination(transform.position);
        }
    }

    public void stopNdoing(float time)
    {
        if (!in_fight)
        {
            stop_timer = time;
            staing = true;
            agent.SetDestination(transform.position);
        }

    }

    void Strange_noise(Vector3 position)
    {
        Debug.Log("NOISE!");
        go_on_noise = true;
        agent.SetDestination(position);
    }

    void Patrul(int i)
    {
        NavMeshPath nav_path = new NavMeshPath();
        agent.SetDestination(patrul_points[i]);
        agent.CalculatePath(patrul_points[i], nav_path);
        if (nav_path.status == NavMeshPathStatus.PathInvalid)
            Debug.Log("Invalid 2");
        if ((patrul_points[i] - transform.position).magnitude < 1.6 || nav_path.status == NavMeshPathStatus.PathInvalid)
        {
            if (patrul_index == patrul_points.Count - 1) patrul_index = 0;
            else patrul_index++;
        }
    }

    void Сhase(Transform player)
    {
        if (!immobil&&!in_fight&&!staing) {
            chasing = true;
            if (player != null)
                player_tr = player;
            agent.SetDestination(player.position);
            agent.speed = 3;
            agent.stoppingDistance = 1f;
        }
    }

    void stop_chase() {
        float shortest_dist = (transform.position - patrul_points[0]).magnitude;
        if(chasing)
            for (int i = 1; i < patrul_points.Count; i++)
                if (shortest_dist > (transform.position - patrul_points[i]).magnitude)
                {
                    shortest_dist = (transform.position - patrul_points[i]).magnitude;
                    patrul_index = i;
                }
        chasing = false;
        agent.speed = 1;
        agent.stoppingDistance = 0f;
        player_tr = null;
    }

    bool go_to_torch()
    {
        bool does_off = false;
        for (int i = 0; i < torchs.Count; i++)
        {
            if (torchs[i].enabled == false)
            {
                agent.SetDestination(torchs[i].transform.position);
                does_off = true;
            }
        }
        return does_off;
    }
}
