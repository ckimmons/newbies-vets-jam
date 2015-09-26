using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TravelWaypoint : MonoBehaviour {

    private static float threshold = .1f;
    private static float trailWidth = .1f;

    public PlayerMovement.playerState trailType;
    public TravelWaypoint next;
    public Material lineMat;
    public Color lineCol;
    public bool active = false;

    private bool hasJunction;
    private Vector3 junctionPosition;
    private TravelWaypoint junctionWaypoint;
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

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
        if (next != null)
        {
            RaycastHit2D hit = Physics2D.Linecast(transform.position, next.transform.position);
            if (hit)
            {
                hasJunction = true;
                junctionPosition = hit.point;
                junctionWaypoint = hit.collider.GetComponent<TravelWaypoint>();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (hasJunction && active)
        {
            float distance = Vector2.Distance(player.transform.position, junctionPosition);
            if(distance < threshold)
            {
                
                if(junctionWaypoint.trailType == PlayerMovement.currentState)
                {
                    player.setCurrentWaypoint(junctionWaypoint);
                }
            }
        }
	}

 
}
