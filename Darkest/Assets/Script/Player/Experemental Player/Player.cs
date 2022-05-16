using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum state
    {
        walck, stay, fly, squat, hide, incapable
    }

    public state player_state;

    SkillTree skills;
    PlayerMove move;


    private void Awake()
    {
        skills = FindObjectOfType<SkillTree>();

        move = new PlayerMove(this,transform , transform.GetComponent<Rigidbody>());
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {

        move.Walck();
        move.Jump();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Graund"))
        {
            player_state = state.stay;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Graund"))
        {
            other.ClosestPoint(transform.position);
            player_state = state.fly;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        player_state = state.stay;
    }

    private void OnCollisionExit(Collision collision)
    {
        player_state = state.fly;
    }
}
