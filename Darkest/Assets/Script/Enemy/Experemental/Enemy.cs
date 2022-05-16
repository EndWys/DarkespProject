using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy
{
    public int HP;
    public float  visabilyty;
    public float alert;
    public int speed;
    public float scared;

    public Enemy(int hp, int vis, int ale, int spe, int sca,Transform player)
    {
        HP = 100;
        visabilyty = 100;
        alert = 0;
        speed = 1;
        scared = 1;
    }
}
