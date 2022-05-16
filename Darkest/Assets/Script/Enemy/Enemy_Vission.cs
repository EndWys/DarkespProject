using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Vission : MonoBehaviour
{
    StatsNsetings player_stats;

    float ray_Dist=7;
    public float visability;
    public bool see_you;
    public Transform player;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vission_ray();
    }
    void Vission_ray()
    {
        for (float r = -0.75f; r < 0.75; r += 0.15f)
        {
            Vector3 ray_target = transform.TransformVector(r * ray_Dist, 0, ray_Dist) + transform.InverseTransformPoint(transform.position.x,-2f,transform.position.z);
            Ray ray = new Ray(transform.position, ray_target);
            Debug.DrawRay(transform.position, ray.direction * ray_Dist);
            if (Physics.Raycast(ray, out hit, ray_Dist, 1))
            {
                if (hit.transform.name == "Player")
                {
                    player_stats = hit.transform.GetComponent<StatsNsetings>();
                    if (!player_stats.hided)
                    {
                        float dist_to_player = (hit.point - transform.position).magnitude / ray_Dist;
                        visability = ((1 - dist_to_player) + player_stats.light_force) / 2;
                        //Debug.Log(visability);
                        see_you = true;
                    }
                    else see_you = false;
                    break;
                }
                else { see_you = false;}
            }
            else see_you = false;
        }
    }
}
