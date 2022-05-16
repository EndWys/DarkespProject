using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SeePlayer 
{
    public void Player(Transform player, Transform enemy)
    {
        EnemyComponent This_enemy;
        enemy.TryGetComponent<EnemyComponent>(out This_enemy);

        if (This_enemy.E.alert < 100)
        {
            for (int i = 0; i < This_enemy.acts.Length; i++)
                if (This_enemy.acts[i] == EnemySystem.ActionList.Chaese)
                    This_enemy.tasks[i] = false;
            This_enemy.E.alert += Time.deltaTime*10;
            Debug.Log(This_enemy.E.alert);
        }
        else
            for (int i = 0; i < This_enemy.acts.Length; i++)
                if (This_enemy.acts[i] == EnemySystem.ActionList.Chaese)
                {
                    This_enemy.tasks[i] = true;
                }
    }
}
