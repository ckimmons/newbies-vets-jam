using UnityEngine;
using System.Collections;

public class ColorController : MonoBehaviour {

    public Color whiteColor = Color.white;
    public Color blackColor = Color.black;

	// Use this for initialization

    void Awake()
    {
        foreach (TravelWaypoint w in GameObject.FindObjectsOfType<TravelWaypoint>())
        {
            if (w.trailType == PlayerMovement.playerState.white)
            {
                w.lineCol = whiteColor;
            }
            else if (w.trailType == PlayerMovement.playerState.black)
            {
                w.lineCol = blackColor;
            }
        }
    }

	void Start () {
	    
	}

    void OnValidate()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
