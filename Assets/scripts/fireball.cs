using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localScale.x, 0) * 5000);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.CompareTag("Landscape") || coll.transform.CompareTag("spikes") || coll.transform.CompareTag("projectile"))
        {
            Destroy(gameObject);
        }
    }
}
