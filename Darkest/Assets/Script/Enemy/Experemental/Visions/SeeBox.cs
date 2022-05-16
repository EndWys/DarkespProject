using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SeeBox
{
    public void Box(Transform box, Transform enemy)
    {
        Debug.Log("box seen");
        EnemyComponent This_enemy;
        Boxes boxes;
        enemy.TryGetComponent<EnemyComponent>(out This_enemy);
        box.TryGetComponent<Boxes>(out boxes);
        if (This_enemy == null)
            Debug.Log("Enemy is null");
        if (boxes.player_stats != null)
        {
            This_enemy.actNfeel.goToBox.box = box;
            for (int i = 0; i < This_enemy.acts.Length; i++)
                if (This_enemy.acts[i] == EnemySystem.ActionList.GoToBox)
                    This_enemy.tasks[i] = true;
        }
    }
}
