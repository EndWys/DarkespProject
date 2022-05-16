using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_controller : MonoBehaviour
{
    Light[] lightos;
    public Light ambiant_light;
    public StatsNsetings player;
     
    Color starter, starter_ambient;
    Color32 skill_use = new Color32(200,160,240,255), skill_color = new Color32(0,80,240,255);

    private void Update()
    {
        if (player.light_force < 0.1f)
        {
            //Debug.Log("SOSS");
            if(ambiant_light.intensity < 28)
            ambiant_light.intensity += Time.deltaTime* 20;
        }
        else if(ambiant_light.intensity > 5)
            ambiant_light.intensity -= Time.deltaTime * 20;
    }

    private void Start()
    {
        lightos = FindObjectsOfType<Light>();
        starter = lightos[0].color;
        starter_ambient = ambiant_light.color;
    }

    public void Using_skill()
    {
        ambiant_light.color = skill_color;
        for (int i=0;i< lightos.Length;i++)
        {
            if(lightos[i]!=null && lightos[i].type != LightType.Directional)
                lightos[i].color = skill_use;
        }
        ambiant_light.intensity = 3;
    }
    public void Skill_used()
    {
        for (int i = 0; i < lightos.Length; i++)
        {
            if (lightos[i] != null && lightos[i].type != LightType.Directional)
                lightos[i].color = starter;
        }
        ambiant_light.color = starter_ambient;
       ambiant_light.intensity = 4;
    }
}
