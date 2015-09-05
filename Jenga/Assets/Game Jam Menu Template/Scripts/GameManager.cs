using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public int score;

	public Dictionary<int,GameObject> barDictionary;


	public class InitPositionInfo
	{
		public Vector3 position;
		public Quaternion rotation;

		public InitPositionInfo (Vector3 vec, Quaternion qua)
		{
			position = vec;
			rotation = qua;
		}
	}


	// Use this for initialization
	void Start () {
		barDictionary = new Dictionary<int, GameObject>();
		init ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.R))
		{
			init ();
		}
	}


	//hard code data for tower building
	//public int numBars = 2;
	//Vector3 [] tower_position_ary = new Vector3[]{new Vector3(5f,0f,0f),new Vector3(1f,20f,0.5f)};
	public InitPositionInfo[] init_state_array = new InitPositionInfo[]{ 
		new InitPositionInfo(new Vector3(0f,0f,0f),Quaternion.Euler(new Vector3(0f,90f,0f))),
		new InitPositionInfo(new Vector3(0f, 0f, 5f),Quaternion.Euler(new Vector3(0f, 90f, 0f)))
		,new InitPositionInfo(new Vector3(0f,10f,0f),Quaternion.identity)
		//,new InitPositionInfo(new Vector3(10f,2f,5f),Quaternion.Euler(new Vector3(0f,90f,20f)) )
	};

	void init()
	{
		//init game
		DestroyAllBars();

		foreach(InitPositionInfo init_state in init_state_array)
		{
			//GameObject.Instantiate(Resources.Load("Bar"),init_state.position,init_state.rotation);
			addBar("Cube",init_state);
		}
	}


	public void upadateScore(int addscore)
	{
		score += addscore;
		Debug.Log (score);
	}

	public void addBar(string bar_name,InitPositionInfo init_state)
	{
		GameObject bar = GameObject.Instantiate(Resources.Load(bar_name),init_state.position,init_state.rotation) as GameObject;
		barDictionary.Add(bar.GetInstanceID(),bar);
	}

	public void DestroyAllBars()
	{
		foreach(KeyValuePair<int, GameObject> pair in barDictionary)
		{
			Destroy(pair.Value);
		}
	}
}
