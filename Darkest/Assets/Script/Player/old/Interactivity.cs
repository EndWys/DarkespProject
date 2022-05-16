using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interactivity : MonoBehaviour
{
    Movement move;
    Animator anim;
    Collider col;
    public Button button;

    private void Start()
    {
        move = transform.GetComponent<Movement>();
        anim = transform.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Inter-activ"|| other.tag == "Inter-active")
        {
            button.gameObject.SetActive(true);
            col = other;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other!=null)
            if (other.tag == "Inter-activ" || other.tag == "Inter-active")
            {
                button.gameObject.SetActive(false);
                col = null;
            }
    }

    public void InterActiv()
    {
        anim.Play("Take" + move.orientation);
        string colls_name = col.name;
        col.SendMessage("Player_Act", transform);
    }
}
