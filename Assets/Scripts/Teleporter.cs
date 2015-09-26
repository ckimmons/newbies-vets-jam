using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour
{
	public Transform teleportTo;
	
	void OnCollisionEnter (Collision col) 
	{
		col.gameObject.transform.position = teleportTo.position;	
	}

	void OnTriggerEnter (Collider col) 
	{
		col.gameObject.transform.position = teleportTo.position;
	}
}
