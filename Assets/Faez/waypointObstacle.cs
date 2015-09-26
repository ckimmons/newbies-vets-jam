using UnityEngine;
using System.Collections;

public class waypointObstacle : MonoBehaviour {
	
	public GameObject start;
	public GameObject end;
	public float speed;
	private float lProgress;
	private float lDistance;
	private bool countUp = true;

	// Use this for initialization
	void Start () {
		lDistance = Vector2.Distance(start.transform.position, end.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (countUp == true) {
			lProgress += Time.deltaTime * speed / lDistance;
			if (lProgress > 1f) {
				// If you really don't want the obstacle moving past its start and end points
				//				lProgress = 1f;
				countUp = false;
			}
		}
		else {
			lProgress -= Time.deltaTime * speed / lDistance;
			if (lProgress < 0f) {
//				lProgress = 0f;
				countUp = true;
			}
	}
		transform.position = Vector2.Lerp (start.transform.position, end.transform.position, lProgress);
		}
		

	}

