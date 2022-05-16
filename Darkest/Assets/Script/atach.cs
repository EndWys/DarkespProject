using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atach : MonoBehaviour
{
    public Transform atach_to;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = atach_to.position;
        transform.rotation = atach_to.rotation;
    }
}
