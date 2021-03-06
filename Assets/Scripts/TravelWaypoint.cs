﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;


public class TravelWaypoint : MonoBehaviour {

    struct junction{
        public Vector3 junctionPosition;
        public TravelWaypoint junctionWaypoint;
    }

    private static float threshold = .1f;
    private static float trailWidth = .1f;

    public PlayerMovement.playerState trailType;
    public TravelWaypoint next;
    public Material lineMat;
    public Color lineCol;
    public bool active = false;

    List<junction> junctions = new List<junction>();
    private PlayerMovement player;

    void OnDrawGizmos()
    {
        if (next != null)
        {
            if(trailType == PlayerMovement.playerState.white)
            {
                Gizmos.color = Color.white;
            }
            else if(trailType == PlayerMovement.playerState.black)
            {
                Gizmos.color = Color.black;
            }
            Gizmos.DrawLine(transform.position, next.transform.position);
            Gizmos.color = Color.gray;
        }
        Gizmos.DrawSphere(transform.position, .2f);
        foreach(junction j in junctions)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(j.junctionPosition, .3f);
        }
    }

    void OnValidate()
    {
        if(next != null)
        {
            
        }
    }

    void Awake () {
        gameObject.layer = getLayer();
        if (next != null)
        {
            EdgeCollider2D col = GetComponent<EdgeCollider2D>();
            if (col == null)
            {
                col = gameObject.AddComponent<EdgeCollider2D>();
            }
            List<Vector2> points = new List<Vector2>();
            points.Add(Vector2.zero);
            points.Add(next.transform.position - transform.position);
            points.Add(Vector2.zero);
            col.points = points.ToArray();

            LineRenderer lr = GetComponent<LineRenderer>();
            if (lr == null)
            {
                lr = gameObject.AddComponent<LineRenderer>();
            }
            lr.material = lineMat;
            lr.SetColors(lineCol, lineCol);
            lr.SetPosition(0, transform.position);
            lr.SetPosition(1, next.transform.position);
            lr.SetWidth(trailWidth, trailWidth);
        }
    }

    int getLayerMask()
    {
        if(trailType == PlayerMovement.playerState.black)
        {
            return 1<<9;
        }
        if(trailType == PlayerMovement.playerState.white)
        {
            return 1<<8;
        }
        return -1;
    }

    int getLayer()
    {
        if (trailType == PlayerMovement.playerState.black)
        {
            
            return (int)PlayerMovement.gameLayers.black;
        }
        if (trailType == PlayerMovement.playerState.white)
        {
            return (int)PlayerMovement.gameLayers.white;
        }
        return -1;
    }

    void Start()
    {
        List<Collider2D> disabledColliders = new List<Collider2D>();
        player = GameObject.FindObjectOfType<PlayerMovement>();
        if (next != null)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, next.transform.position,getLayerMask());
            Debug.DrawLine(transform.position, next.transform.position, Color.red,10);
            while (hit)
            {
                junction junct = new junction();
                junct.junctionPosition = hit.point;
                junct.junctionWaypoint = hit.collider.GetComponent<TravelWaypoint>();
                junctions.Add(junct);
                disabledColliders.Add(hit.collider);

                hit.collider.enabled = false;
                
                hit = Physics2D.Linecast(transform.position, next.transform.position);
            }
           foreach(Collider2D col in disabledColliders)
            {
                col.enabled = true;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (true)
        {
            for (int k=0;k < junctions.Count; k++)
            {
                junction j = junctions[k];
                float distance = Vector2.Distance(player.transform.position, j.junctionPosition);
                if (distance < threshold)
                {
                    if (j.junctionWaypoint.trailType == PlayerMovement.currentState)
                    {
                        player.setCurrentWaypoint(j.junctionWaypoint);
                    }
                }
            }
        }
        
	}

 
}
