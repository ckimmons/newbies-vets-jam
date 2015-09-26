using UnityEngine;
using System.Collections;

public class CuteJitter : MonoBehaviour {

	public 	float speed;
	public Vector3 rotationAmount = new Vector3(0,0,45);

	Vector3 startAng = Vector3.zero;

	// Use this for initialization
	void Start () {
		this.startAng = this.transform.localEulerAngles;
		this.speed = Random.Range(3,9);
	}
	
	// Update is called once per frame
	void Update () {
		float twitchFactor = Mathf.Sin(Time.time * speed);
		twitchFactor = Mathf.RoundToInt(twitchFactor * 1.5f) / 1.5f;
		this.transform.localEulerAngles = startAng + rotationAmount *.5f*twitchFactor;
	}
}
