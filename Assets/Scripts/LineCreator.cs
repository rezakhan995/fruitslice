﻿using UnityEngine;
using System.Collections;

public class LineCreator : MonoBehaviour
{


	int vertextCount = 0;
	bool mouseDown = false;
	LineRenderer line;
	public GameObject blast;

	void Awake ()
	{
		line = GetComponent<LineRenderer> ();
	}
	// Use this for initialization
	void Start ()
	{
	
	}
	 
	// Update is called once per frame
	void Update ()
	{

		if (Application.platform == RuntimePlatform.Android) {
			if (Input.touchCount > 0) {
				if (Input.GetTouch (0).phase == TouchPhase.Moved) {
					line.SetVertexCount (vertextCount + 1);
					Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					line.SetPosition (vertextCount, mousePos);
					vertextCount++;

					BoxCollider2D box = gameObject.AddComponent<BoxCollider2D> ();
					box.transform.position = line.transform.position;
					box.size = new Vector2 (0.1f, 0.1f);
				}

				if (Input.GetTouch (0).phase == TouchPhase.Ended) {
					vertextCount = 0;
					line.SetVertexCount (0);

					BoxCollider2D[] colliders = GetComponents<BoxCollider2D> ();
					foreach (BoxCollider2D box in colliders) {
						Destroy (box);
					}
				}
			}
		} 

		//else if (Application.platform == RuntimePlatform.WindowsPlayer) {
		else{
			if (Input.GetMouseButtonDown (0)) {
				mouseDown = true;
			}

			if (mouseDown) {
				line.SetVertexCount (vertextCount + 1);
				Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				line.SetPosition (vertextCount, mousePos);
				vertextCount++;

				BoxCollider2D box = gameObject.AddComponent<BoxCollider2D> ();
				box.transform.position = line.transform.position;
				box.size = new Vector2 (0.1f, 0.1f);
			}
			
			if (Input.GetMouseButtonUp (0)) {
				mouseDown = false;
				vertextCount = 0;
				line.SetVertexCount (0);

				BoxCollider2D[] colliders = GetComponents<BoxCollider2D> ();
				foreach (BoxCollider2D box in colliders) {
					Destroy (box);
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if( col.gameObject.tag == "Bomb" && !FruitScript.instance.gameOver){
			GameObject b = Instantiate (blast, col.transform.position, Quaternion.identity) as GameObject;
			Destroy (b.gameObject, 5f);
			Destroy (col.gameObject);
			GameManager.instance.GameOver ();
			FruitScript.instance.gameOver = true;
		}
	}
}
