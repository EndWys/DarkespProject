using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class count_Ilumination : MonoBehaviour
{
    public float lenth_ray;
    LayerMask _default;

    private void Start()
    {
        _default = LayerMask.GetMask("Default");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            light_indicat(other,QueryTriggerInteraction.Ignore);
        }
        if (other.name == "Light_counter")
        {
            light_indicat(other,QueryTriggerInteraction.Collide);
        }
    }

    void light_indicat(Collider other,QueryTriggerInteraction trigers)
    {
        Vector3 dir = (other.transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, dir);
        Debug.DrawRay(ray.origin, ray.direction * lenth_ray);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, lenth_ray, _default, trigers))
            if (hit.collider.name == other.name)
            {
                if (gameObject.GetComponent<Light>().enabled)
                {
                    Vector3 vector_to_light = hit.point - transform.position;
                    float distance = vector_to_light.magnitude / lenth_ray;
                    hit.collider.BroadcastMessage("Ilumination", 1 - distance);
                    //Debug.Log(hit.collider.name);
                }
                else
                    hit.collider.BroadcastMessage("Ilumination", -0.1f);
            }
    }
}
