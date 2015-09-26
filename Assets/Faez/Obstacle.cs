using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

    public PlayerMovement.playerState collisionColor;

	// Use this for initialization
	void Start () {
        ColorController controller = GameObject.FindObjectOfType<ColorController>();
        if (collisionColor == PlayerMovement.playerState.black)
        {
            gameObject.layer = (int)PlayerMovement.gameLayers.black;
            GetComponent<Renderer>().material.color = controller.blackColor;
        }
        else if(collisionColor == PlayerMovement.playerState.white)
        {
            gameObject.layer = (int)PlayerMovement.gameLayers.white;
            GetComponent<Renderer>().material.color = controller.whiteColor;
        }
	}   

    void OnValidate()
    {
        gameObject.tag = "obstacle";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
