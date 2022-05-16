using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScrpt : MonoBehaviour
{
    public Transform player;
    float smoothing = 0.125f;
    public float lvl_bord_l, lvl_bord_r;
    Vector3 offset,target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x <= lvl_bord_l - 1)
            target = new Vector3(lvl_bord_l - 1, transform.position.y, transform.position.z);
        else if(player.position.x >= lvl_bord_r + 1)
            target = new Vector3(lvl_bord_r + 1, transform.position.y, transform.position.z);
        else
            target = new Vector3(player.position.x, transform.position.y, transform.position.z);

        smooth_camera(target);
    }
    void smooth_camera(Vector3 target)
    {
        transform.position = Vector3.Lerp(transform.position, target, 5f * Time.deltaTime);
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.position.x, transform.position.y, transform.position.z), 0.1f);
    }
}
