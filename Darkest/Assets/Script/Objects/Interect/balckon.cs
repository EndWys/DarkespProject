using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class balckon : MonoBehaviour
{
    Enemy_stat enemy;
    Collider[] hitColliders;
    // Start is called before the first frame update
    void fall_whith()
    {
        if (enemy != null)
        {
            enemy.animation.Play("FallFront_L");
            enemy.SendMessage("death");
        }
        transform.parent.GetChild(1).gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Collision")
            enemy = other.transform.GetComponentInParent<Enemy_stat>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Collision")
            enemy = null;
    }

    void make_noice()
    {
        hitColliders = Physics.OverlapSphere(transform.position, 20, LayerMask.GetMask("Default"), QueryTriggerInteraction.Collide);
        for (int i = 0; i < hitColliders.Length; i++)
            if (hitColliders[i].name == "Collision")
                hitColliders[i].transform.SendMessageUpwards("Strange_noise", transform.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            make_noice();
            Destroy(this, 0.2f);
            Destroy(gameObject, 0.4f);
        }
    }
}
