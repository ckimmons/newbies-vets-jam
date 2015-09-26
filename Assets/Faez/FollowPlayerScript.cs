using UnityEngine;
using System.Collections;

public class FollowPlayerScript : MonoBehaviour {

    Transform player;
    Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
        player = GameObject.FindObjectOfType<PlayerMovement>().transform;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z), ref velocity, .2f);
	}
    
}
