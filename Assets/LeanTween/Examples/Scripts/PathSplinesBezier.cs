using UnityEngine;
using System.Collections;

public class PathSplinesBezier : MonoBehaviour {

    public Transform[] trans;
    //public BezierSpline spline;

    public float speed = 10;

	LTBezierPath cr;
	private GameObject avatar1;

	void OnEnable(){
        // create the path
        cr = new LTBezierPath( new Vector3[] {trans[0].position, trans[2].position, trans[1].position, trans[3].position, trans[3].position, trans[5].position, trans[4].position, trans[6].position} );

    }

	void Start () {
		avatar1 = GameObject.Find("Avatar1");

		// Tween automatically
		LTDescr descr = LeanTween.move(avatar1, cr.pts, cr.length / speed).setOrientToPath(true).setRepeat(0);
		Debug.Log("length of path 1:"+cr.length);
		Debug.Log("length of path 2:"+descr.path.length);
	}
	
	private float iter;
	void Update () {
		// Or Update Manually
		//cr.place2d( sprite1.transform, iter );

		iter += Time.deltaTime*0.07f;
		if(iter>1.0f)
			iter = 0.0f;
	}

	void OnDrawGizmos(){
        // Debug.Log("drwaing");
        OnEnable();
		Gizmos.color = Color.red;
		if(cr!=null)
			cr.gizmoDraw(); // To Visualize the path, use this method
	}
}
