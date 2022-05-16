using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float in_air = 1f, from_wall=0, on_stairs = 0;
    bool on_ground=true, walcking;

    Rigidbody r_body;
    Collider colls;
    Animator animator;
    StatsNsetings player_stats;

    Vector3 Hook_pos;
    public string orientation = "_L";
    bool animated_state = false, pull_up,on_hook=false;//kinematik Riged body and controld by animation of transform
    
    void Start()
    {
        r_body = GetComponent<Rigidbody>();
        colls = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        player_stats = GetComponent<StatsNsetings>();
    }

    // Update is called once per frame
    void Update()
    {
        if (on_hook)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (pull_up)
                {
                    animated_state = true;
                    animator.Play("Hook" + orientation);
                }
                else
                {
                    r_body.isKinematic = true;
                    StartCoroutine(ready_to_pull());
                }
            }
            else if (Input.GetKeyDown(KeyCode.S) && pull_up)
                r_body.isKinematic = false;
        }
    }

    IEnumerator ready_to_pull()
    {
        yield return new WaitForFixedUpdate();
        pull_up = true;
    }


    private void FixedUpdate()
    {
        if (!player_stats.hided)
        {
            walcking = false;
            
            if (Input.GetKey(KeyCode.A))
            {
                orientation = "_L";
                walcking = true;
                animator.SetInteger("Orientation", 0);
                r_body.AddForce(new Vector3((-300 + on_stairs) * in_air * Time.deltaTime, 0 + on_stairs, 0));
            }

            if (Input.GetKey(KeyCode.D))
            {
                orientation = "_R";
                walcking = true;
                animator.SetInteger("Orientation", 1);
                r_body.AddForce(new Vector3((300 - on_stairs) * in_air * Time.deltaTime, 0 + on_stairs, 0));
            }

            if (Input.GetKey(KeyCode.W))
            {
                walcking = true;
                r_body.AddForce(new Vector3(0, 0, 300 * Time.deltaTime * in_air));
            }

            if (Input.GetKey(KeyCode.S))
            {
                walcking = true;
                r_body.AddForce(new Vector3(0, 0, -300 * Time.deltaTime * in_air));
            }

            animator.SetBool("walck", walcking);

            if (Input.GetKey(KeyCode.Space))
                if (on_ground)
                {
                    animator.SetBool("walck", false);
                    r_body.velocity = Vector3.zero;
                    if(from_wall==0)
                        animator.Play("jump" + orientation, 0);
                    else animator.Play("From wall" + orientation, 0);
                    r_body.AddForce(new Vector3(from_wall, 400 - (Mathf.Abs(from_wall) * 2), 0));
                    on_ground = false;
                }
        }

        if (animated_state)
        {
            if(transform.position.y < Hook_pos.y)
            {
                transform.position = Vector3.MoveTowards(transform.position, Hook_pos, 5 * Time.deltaTime);
            }
            else
            {
                animated_state = false;
                r_body.isKinematic = false;
            }
            if(transform.position.x > Hook_pos.x)
            {

            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Platform")
        {
            on_ground = true;
            animator.SetTrigger("fall");
        }
        in_air = 1;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Ground")
        {
            on_ground = true;
            animator.SetTrigger("fall");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Ground"|| collision.collider.tag == "Platform")
        {
            in_air = 0.8f;
            on_ground = false;
            animator.ResetTrigger("fall");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Grab")
        {
            on_ground = true;
            if (other.name == "grab_l")
                from_wall = -40;
            else if (other.name == "grab_r")
                from_wall = 40;
        }
        if (other.name == "Stairs_triger")
            on_stairs = 14;
        if (other.name == "hook")
        {
            on_hook = true;
            pull_up = false;
            Hook_pos = other.transform.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Grab")
        {
            from_wall = 0;
            on_ground = false;
        }
        if (other.name == "Stairs_triger")
            on_stairs = 0;
        if (other.name == "hook")
            on_hook = false;
    }

    public void Punch(int oritation)
    {
        r_body.velocity = Vector3.zero;
        r_body.AddForce(new Vector3(oritation, 120, 0));
        on_ground = false;
    }
}
