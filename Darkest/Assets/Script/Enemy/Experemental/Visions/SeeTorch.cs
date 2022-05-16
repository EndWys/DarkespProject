using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SeeTorch
{
    public void Torch(Transform torch,Transform enemy)
    {
        EnemyComponent This_enemy;
        Light light;
        enemy.TryGetComponent<EnemyComponent>(out This_enemy);
        if (This_enemy == null)
            Debug.Log("Enemy is null");
        torch.TryGetComponent<Light>(out light);
        if(light == null)
            Debug.Log("Torch is null");
        if (!light.enabled)
        {
            This_enemy.actNfeel.extinctTorch.torch = torch;
            for (int i = 0; i < This_enemy.acts.Length; i++)
                if (This_enemy.acts[i] == EnemySystem.ActionList.ExtinctTorch)
                    This_enemy.tasks[i] = true;
        }
    }
}
