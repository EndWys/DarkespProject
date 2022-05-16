using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove
{

    Player player;
    Transform transform;
    Rigidbody rb;

    Transform detector;

    //bool on_ground = true;

    public PlayerMove(Player pler,Transform tr, Rigidbody rigidbody)
    {
        rb = rigidbody;
        transform = tr;
        player = pler;
    }

    public void Walck()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(new Vector3((-300) * Time.deltaTime, 0, 0));
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(new Vector3((300) * Time.deltaTime, 0, 0));
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(new Vector3(0, 0, 300 * Time.deltaTime));
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(new Vector3(0, 0, -300 * Time.deltaTime));
        }
        
    }

    public void Jump()
    {
        if (Input.GetKey(KeyCode.Space))
            if (player.player_state != Player.state.fly)
            {
                rb.velocity = Vector3.zero;
                //if (from_wall == 0)
                //    animator.Play("jump" + orientation, 0);
                //else animator.Play("From wall" + orientation, 0);
                //rb.AddForce(new Vector3(from_wall, 400 - (Mathf.Abs(from_wall) * 2), 0));
                rb.AddForce(new Vector3(0, 400, 0));
                player.player_state = Player.state.fly;
            }
    }

    public void Grab()
    {

    }
}