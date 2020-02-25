using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBigMan : MonoBehaviour
{
    public GameObject projectile;
    public float spawnTimer = 3;

    private Transform player;

    public Camera cam1;
    public Camera cam2;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            cam1.enabled = false;
            cam2.enabled = true;
            player = coll.transform;
            StartCoroutine(destroyer());
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            cam1.enabled = true;
            cam2.enabled = false;
            StopCoroutine(destroyer());
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    private void creator()
    {
        GameObject a = Instantiate(projectile) as GameObject;
        a.transform.position = transform.position;
        a.GetComponent<Rigidbody2D>().AddForce((player.transform.position - a.transform.position) * 50);
    }

    IEnumerator destroyer()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTimer);
            creator();
        }
    }
}
