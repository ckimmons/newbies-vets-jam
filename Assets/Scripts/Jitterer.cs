using UnityEngine;
using System.Collections;

public class Jitterer : MonoBehaviour {
	
	Vector3 startPos;
	float amount = 1;
	float speed = 10;
	// Use this for initialization
	void Start () {
		this.startPos = this.transform.localPosition;
		StartCoroutine("jitter");
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	IEnumerator jitter()
	{
		for (;;)
		{
			this.transform.localPosition = startPos + Random.insideUnitSphere * amount / 5.0f;
			yield return new WaitForSeconds(1 / (speed +.05f));
		}
	}
}
