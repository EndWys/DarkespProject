using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rnage_img : MonoBehaviour
{
    public Transform player;
    RectTransform rect_t;

    Vector3 target, local_player;

    void Start()
    {
        rect_t = transform.GetComponent<RectTransform>();
    }

    void Update()
    {
        local_player = transform.InverseTransformPoint(player.position);
        target = transform.TransformPoint(new Vector3(local_player.x, -140 + (100 * (player.position.z + 2.5f) / 4) + local_player.y +60f, 0));
        rect_t.position = Vector3.Lerp(rect_t.position, target, 40f * Time.deltaTime);
    }
}
