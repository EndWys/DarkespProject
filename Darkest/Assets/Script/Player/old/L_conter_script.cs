using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_conter_script : MonoBehaviour
{

    public float light_force;
    float forces = 0;

    void FixedUpdate()
    {
        light_force = forces;
        forces = 0;
    }

    void Ilumination(float light)
    {
        if (light < 0)
            light = 0;
        forces += light;
        //StartCoroutine(calculat_light());
    }
}
