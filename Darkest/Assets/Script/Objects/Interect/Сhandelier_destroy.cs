using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Сhandelier_destroy : MonoBehaviour
{
    public Collider[] hitColliders;

    void make_noice()
    {
        hitColliders = Physics.OverlapSphere(transform.position, 20, LayerMask.GetMask("Default"), QueryTriggerInteraction.Collide);
        for (int i = 0; i < hitColliders.Length; i++)
            if (hitColliders[i].name == "Collision")
                hitColliders[i].transform.SendMessageUpwards("Strange_noise", transform.position);
    }
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            make_noice();
            Destroy(gameObject, 1f);
        }
    }
}
