using UnityEngine;
using System.Collections;

public class Bar : MonoBehaviour {

	public int value;
	public GameManager gameManager;


	public bool value_enabled = true;


	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find ("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if(Input.GetMouseButtonDown(0))
		{
			//if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),)
			{
				GetComponent<Rigidbody>().AddForce(Vector3.forward*100);
			}
		}
		*/
	}

	void OnMouseDown()
	{
		GetComponent<Rigidbody>().AddForce(transform.forward*100);
	}


	void OnCollisionEnter(Collision col)
	{
		if(value_enabled)
		{
			if(col.gameObject.tag == "Ground" )
			{
				//Debug.Log ("hit the ground");
				gameManager.upadateScore(value);
				value_enabled = false;
			}
		}
	}
}
