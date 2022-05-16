using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam_fall : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ground")
            Destroy(gameObject, 0.2f);
    }
}
