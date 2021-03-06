﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    public enum playerState
    {
        black,
        white,
        gray
    }

    public enum gameLayers
    {
        gray = 1,
        black = 8,
        white = 9,
        blackPlayer = 10,
        whitePlayer = 11
    }

    public static playerState currentState = playerState.black;
    public GameObject wonObject;

    public float slowMovementSpeed;
    public float normalMovementSpeed;
    public float fastMovementSpeed;

    private float movementSpeed;

    public TravelWaypoint startWaypoint;

    private Material mat;
    private TravelWaypoint currentWaypoint;
    private TravelWaypoint targetWaypoint;
    private bool won;
    private ColorController controller;

	// Use this for initialization
	void Start () {
        transform.position = startWaypoint.transform.position;
        setCurrentWaypoint(startWaypoint);
        mat = GetComponent<Renderer>().material;
        controller = GameObject.FindObjectOfType<ColorController>();
        updateStatesBasedOnColor();
    }
	
    void moveTowardsWaypoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.transform.position, movementSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, targetWaypoint.transform.position) < .2f)
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
            handleInput();
        }
    }

    void handleInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movementSpeed = slowMovementSpeed;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {

            movementSpeed = fastMovementSpeed;
        }
        else
        {

            movementSpeed = normalMovementSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            toggleState();
        }

    }

    void toggleState()
    {
        if (currentState == playerState.black)
        {
            currentState = playerState.white;
        }
        else if (currentState == playerState.white)
        {
            currentState = playerState.black;
        }
        updateStatesBasedOnColor();
    }

    void updateStatesBasedOnColor()
    {
        if(currentState == playerState.black)
        {
            gameObject.layer = (int)gameLayers.blackPlayer;
            mat.color = controller.blackColor;
        }
        else if(currentState == playerState.white) {
            gameObject.layer = (int)gameLayers.whitePlayer;
            mat.color = controller.whiteColor;
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
        Instantiate(wonObject, transform.position, Quaternion.identity);
    }
}
