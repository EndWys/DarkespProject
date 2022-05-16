using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    public EnemyComponent[] Enemies;
    // Start is called before the first frame update 

    void Awake()
    {
        Enemies = GameObject.FindObjectsOfType<EnemyComponent>();
    }
}
