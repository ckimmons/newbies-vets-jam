using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public enum playerState
    {
        black,
        white
    }

    public static playerState currentState = playerState.black;

    public float movementSpeed;
    public TravelWaypoint startWaypoint;

    private TravelWaypoint currentWaypoint;
    private TravelWaypoint targetWaypoint;
    private bool won;

	// Use this for initialization
	void Start () {
        transform.position = startWaypoint.transform.position;
        setCurrentWaypoint(startWaypoint);
	}
	
    void moveTowardsWaypoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.transform.position, movementSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, targetWaypoint.transform.position) == 0)
        {
            targetWaypoint = targetWaypoint.next;
            if (targetWaypoint == null)
            {
                playerWon();
            }
        }
    }

	// Update is called once per frame
	void Update () {
        if (!won)
        {
            moveTowardsWaypoint();
        }
    }

    //sets the current waypoint we are starting at
    public void setCurrentWaypoint(TravelWaypoint current)
    {
        if(currentWaypoint != null)
            currentWaypoint.active = false;
        currentWaypoint = current;
        currentWaypoint.active = true;
        targetWaypoint = currentWaypoint.next;
    }

    void playerWon()
    {
        won = true;
    }
}
