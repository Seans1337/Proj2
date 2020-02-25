using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonChaser : MonoBehaviour
{
	[SerializeField] GameObject m_toSpawn;

	private bool canSpawn;

	void Awake()
	{
		canSpawn = true;
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.CompareTag("Player") && canSpawn)
		{
			Instantiate(m_toSpawn, transform.position, transform.rotation);
            canSpawn = false;
		}
	}
}
