using UnityEngine;
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

    }

    void OnValidate()
    {
        if(next != null)
        {
            
        }
    }

    void Awake () {

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
            return (int)PlayerMovement.gameLayers.white;
        }
        if(trailType == PlayerMovement.playerState.white)
        {
            return (int)PlayerMovement.playerState.black;
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
            return (int)PlayerMovement.playerState.white;
        }
        return -1;
    }

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        if (next != null)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, next.transform.position,getLayerMask());
            while(hit)
            {
                Debug.Log(name + " " + hit.collider.name);
                junction junct = new junction();
                junct.junctionPosition = hit.point;
                junct.junctionWaypoint = hit.collider.GetComponent<TravelWaypoint>();
                junctions.Add(junct);
                hit.collider.enabled = false;
                hit = Physics2D.Linecast(transform.position, next.transform.position);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            foreach (junction j in junctions)
            {
                float distance = Vector2.Distance(player.transform.position, j.junctionPosition);
                if (distance < threshold)
                {
                    Debug.Log("juncture");
                    if (j.junctionWaypoint.trailType == PlayerMovement.currentState)
                    {
                        player.setCurrentWaypoint(j.junctionWaypoint);
                    }
                }
            }
        }
        
	}

 
}
