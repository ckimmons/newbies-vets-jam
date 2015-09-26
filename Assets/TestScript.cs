using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("I pressed space");
        }
	}

    void OnCollisionEnter2D(Collision2D buts)
    {
        Debug.Log("Collision Occured");
    }

    void OnTriggerEnter2D(Collider2D asdf)
    {

        Debug.Log("Trigger collision occured");
    }
}
