using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trump2020 : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.CompareTag("Landscape") || coll.transform.CompareTag("spikes"))
        {
            Destroy(gameObject);
        }
    }
}
