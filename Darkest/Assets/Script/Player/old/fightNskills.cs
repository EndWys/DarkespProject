using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fightNskills : MonoBehaviour
{
    Ray pointer_ray;
    RaycastHit pointer_hit;
    public Camera main_camera;
    public L_conter_script l_Conter;
    public Light_controller light_controller;
    public Transform SkillBox;
    public Text InHand;

    Transform attack_target;

    Rigidbody rb;
    Animator anim;
    StatsNsetings stats;
    Movement move;
    public int in_hand = 0;//1 sword , 2 rope;
    bool skill_1_timer, in_range;
    float skill_2_timer, skill_4_timer, target_ilum;


    public float[] range = new float[4] {3,4,9,3};
    float[] coldowns = new float[4] { 2, 20, 24, 16 };
    public float[] coldowns_time = new float[4] {2,20,24,16};
    public Image[] coldown_img = new Image[4];
    public RectTransform range_image_rect, shadow_force1,shadow_force2;

    private void Start()
    {
        anim = transform.GetComponent<Animator>();
        rb = transform.GetComponent<Rigidbody>();
        stats = transform.GetComponent<StatsNsetings>();
        move = transform.GetComponent<Movement>();
    }

    private void Update()
    {
        switch (in_hand)
        {
            case 1: InHand.text = "You have a sword"; break;
            case 2: InHand.text = "You have a rope"; break;
            case 3: InHand.text = "You have a dagger"; break;
            default: InHand.text = "You have nothing"; break;
        }

        shadow_force1.sizeDelta = new Vector2(20,100*stats.light_force); 
        shadow_force2.sizeDelta = new Vector2(20, 100 * target_ilum);

        if (Input.GetKeyDown(KeyCode.Q)&& in_range)
            attack(attack_target);

        for (int i=0; i < 4; i++)
        {
            if (coldowns[i] < coldowns_time[i])
            coldowns[i] += Time.deltaTime;
            coldown_img[i].fillAmount = coldowns[i] / coldowns_time[i];
        }

        if (coldowns[0] >= coldowns_time[0])
            if (Input.GetKey(KeyCode.Alpha1)) {
                GetReady_toSkill(0);
                if(Input.GetMouseButtonDown(0))
                    skill_1();
            }
            else if (Input.GetKeyUp(KeyCode.Alpha1))
                cancel_skill();
        if (coldowns[1] >= coldowns_time[1])
            if (Input.GetKey(KeyCode.Alpha2))
            {
                GetReady_toSkill(1);
                if (Input.GetMouseButtonDown(0))
                    skill_2();
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
                cancel_skill();
        if (coldowns[2] >= coldowns_time[2])
            if (Input.GetKey(KeyCode.Alpha3))
            {
                GetReady_toSkill(2);
                if (Input.GetMouseButtonDown(0))
                    skill_3();
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
                cancel_skill();
        if (coldowns[3] >= coldowns_time[3])
            if (Input.GetKey(KeyCode.Alpha4))
            {
                GetReady_toSkill(3);
                if (Input.GetMouseButtonDown(0))
                    skill_4();
            }
            else if (Input.GetKeyUp(KeyCode.Alpha4))
                cancel_skill();

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.name == "Collision")
        {
            in_range = true;
            attack_target = other.transform.parent;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Collision")
             in_range = false;
    }

    void attack(Transform target)
    {
        object[] value = new object[2];
        value[0] = in_hand;
        value[1] = transform;

        if(target!=null)
        switch (in_hand)
        {
            case 1:
                //animation clip
                Invoke("make_noise", 0.4f);
                anim.Play("Attack-Sword" + move.orientation);
                target.SendMessage("attacked_normal", value);
                in_hand = 0;
                break;
            case 2:
                //animation clip
                //target.transform.position += Vector3(0, 3, 0); // may be in animation
                target.SendMessage("attacked_normal", value);
                in_hand = 0;
                break;
            case 3:
                //animation clip
                //target.transform.position += Vector3(0, 3, 0); // may be in animation
                anim.Play("Attack-Dagger" + move.orientation);
                target.SendMessage("attacked_normal", value);
                in_hand = 0;
                break;
            default:
                target.SendMessage("attacked_normal", value);
                break;
        }
    }

    public void take_a_hit()
    {
        //animation of bumping back
        //Debug.Log("Attack - fail");
        stats.hp -= 30;
    }
    
    void make_noise()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 20);
        for (int i = 0; i < hitColliders.Length; i++)
            if (hitColliders[i].name == "Collision")
                hitColliders[i].transform.SendMessageUpwards("Strange_noise", transform.position);
    }

    void skill_1()
    {
        Ray ray_skill;
        Vector3 target_point = transform.position;
        pointer_ray = main_camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(pointer_ray.origin, pointer_ray.direction * 100);
        RaycastHit skill_hit;
        if (Physics.Raycast(pointer_ray, out pointer_hit, 1000, LayerMask.GetMask("Default")))
            target_point = pointer_hit.point;
        ray_skill = new Ray(transform.position, (target_point - transform.position).normalized);
        if (Physics.Raycast(ray_skill, out skill_hit))
            if ((pointer_hit.point - new Vector3(transform.position.x, transform.position.y, pointer_hit.point.z)).magnitude < range[0])
            {
                SkillBox.SendMessage("using_skill1_a", skill_hit.point);
                SkillBox.GetChild(0).position = transform.position;
                skill_1_timer = true;
                rb.isKinematic = true;
                StartCoroutine(tentacl(skill_hit.point + new Vector3(0, 1, 0), true));
                coldowns[0] = 0; 
            }
    }

    IEnumerator tentacl(Vector3 target,bool first_time)
    {
        if (first_time)
            yield return new WaitForSeconds(0.2f);

        yield return new WaitForFixedUpdate();
        if (skill_1_timer)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 20 * Time.deltaTime);
            if ((target - transform.position).magnitude > 0.5f)
                StartCoroutine(tentacl(target, false));
            else { cancel_skill(); rb.isKinematic = false; }
        }
    }

    void skill_2()
    {
        Vector3 target_point = transform.position;
        pointer_ray = main_camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(pointer_ray.origin, pointer_ray.direction * 100);
        if (Physics.Raycast(pointer_ray, out pointer_hit, 1000, LayerMask.GetMask("Default"), QueryTriggerInteraction.Collide))
            if((pointer_hit.point - new Vector3(transform.position.x,transform.position.y,pointer_hit.point.z)).magnitude < range[1])
                if (pointer_hit.transform.parent.name == "Torch")
                {
                    Light target_tourch = pointer_hit.transform.parent.GetComponent<Light>();
                    skill_2_timer = 1.5f;
                    SkillBox.SendMessage("using_skill2", pointer_hit.transform.position);
                    StartCoroutine(turns_off(target_tourch));
                    coldowns[1] = 0;
                }
    }

    IEnumerator turns_off(Light torch)
    {
        yield return new WaitForFixedUpdate();
        if (skill_2_timer > 0)
        {
            skill_2_timer -= Time.deltaTime;
            StartCoroutine(turns_off(torch));
        }
        else { torch.enabled = false; cancel_skill();}
    }

    void skill_3()
    {
        Vector3 target_point = transform.position;
        pointer_ray = main_camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(pointer_ray.origin, pointer_ray.direction * 100);
        if (Physics.Raycast(pointer_ray, out pointer_hit, 1000, LayerMask.GetMask("Default")))
            if ((pointer_hit.point - new Vector3(transform.position.x, transform.position.y, pointer_hit.point.z)).magnitude < range[2])
                if (pointer_hit.collider.tag == "Ground")
                {
                    target_point = pointer_hit.point;
                    l_Conter.transform.position = target_point;
                    target_ilum = l_Conter.light_force;
                    l_Conter.transform.position = new Vector3(0, -20, 0);
                    if (target_ilum < 0.15f && stats.light_force < 0.2f)
                    {
                        stats.Hideing();
                        anim.Play("TurningInShadow");
                        StartCoroutine(shadow_dive(target_point + new Vector3(0, 1, 0),true));
                        coldowns[2] = 0;
                    }
                }
    }

    IEnumerator shadow_dive(Vector3 target,bool first_time)
    {
        if (first_time)
            yield return new WaitForSeconds(0.5f);
        yield return new WaitForFixedUpdate();
        transform.position = Vector3.MoveTowards(transform.position, target, 10 * Time.deltaTime);
        if ((target - transform.position).magnitude > 0.5f)
            StartCoroutine(shadow_dive(target,false));
        else { stats.Hideing(); cancel_skill(); }
    }

    void skill_4()
    {
        pointer_ray = main_camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(pointer_ray.origin, pointer_ray.direction * 100);
        if (Physics.Raycast(pointer_ray, out pointer_hit, 1000, LayerMask.GetMask("Default"), QueryTriggerInteraction.Collide))
            if ((pointer_hit.point - new Vector3(transform.position.x, transform.position.y, pointer_hit.point.z)).magnitude < range[1])
                if (pointer_hit.collider.name == "Collision")
                {
                    l_Conter.transform.position = pointer_hit.transform.position;
                    target_ilum = l_Conter.light_force;
                    if (stats.light_force < 0.15f&& target_ilum<0.2f)
                    {
                        skill_4_timer = 3;
                        Transform enemy = pointer_hit.transform.parent;
                        enemy.SendMessage("attaked_skill4");
                        SkillBox.SendMessage("using_skill4", pointer_hit.transform.position);
                        StartCoroutine(hands(enemy));
                        coldowns[3] = 0;
                    }
                }
    }
    IEnumerator hands(Transform enemy)
    {
        yield return new WaitForFixedUpdate();
        if (skill_4_timer > 0)
        {
            skill_4_timer -= Time.deltaTime;
            StartCoroutine(hands(enemy));
        }
        else cancel_skill();
    }


    void GetReady_toSkill(int i)
    {
        light_controller.Using_skill();
        range_image_rect.gameObject.SetActive(true);
        range_image_rect.sizeDelta = new Vector2(range[i] * 1000, range[i] * 1000);
        Vector3 target_point = transform.position;
        pointer_ray = main_camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(pointer_ray.origin, pointer_ray.direction * 100);
        Ray ray_skill;
        if (Physics.Raycast(pointer_ray, out pointer_hit, 1000, 1))
            target_point = pointer_hit.point;
        ray_skill = new Ray(transform.position, (target_point - transform.position).normalized);
        Debug.DrawRay(ray_skill.origin, ray_skill.direction * (target_point - transform.position).magnitude, Color.red);
        l_Conter.transform.position = target_point;
        target_ilum = l_Conter.light_force; 
    }

    void cancel_skill()
    {
        light_controller.Skill_used();
        range_image_rect.gameObject.SetActive(false);
    }
}
