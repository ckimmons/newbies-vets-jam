/*
Destroys the game object on collision, 
assuming the object has a collider with 'Is Trigger' enabled.

Colliding object unaffected
*/
using UnityEngine;
using System.Collections;

	
//requires that the game object has a collider
[RequireComponent (typeof (Collider))]
public class DestroyOnTrigger : MonoBehaviour 
{
	

	void Start () {
	
	}
	
	void Update () {
	
	}
	
	//This function will get called upon collision with
	// another object, as long as this object has a
	// trigger enabled collider
	
	//More information:
	//http://docs.unity3d.com/Documentation/ScriptReference/Collider.OnTriggerEnter.html
   void OnTriggerEnter(Collider other) 
	{
		//Destroy this game object, and all its components
        Destroy(this.gameObject);
    }
}
