using UnityEngine;
using System.Collections;

public class playerCollisionDetector: MonoBehaviour {
	public GameObject gameOverText;
	public enum gameplayState {menu, playing, gameOver}
	public static gameplayState currentState;
		 

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R))
		{
			Debug.Log("Restart attempted");
			if (currentState == gameplayState.gameOver)
			currentState = gameplayState.playing;
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void OnCollisionEnter2D (Collision2D Col) {
		if (Col.gameObject.CompareTag ("obstacle"))
			gameObject.SetActiveRecursively(false);
			currentState = gameplayState.gameOver;
			Instantiate (gameOverText, gameOverText.transform.position, Quaternion.identity);
			Destroy (gameOverText,5);
	}
}
