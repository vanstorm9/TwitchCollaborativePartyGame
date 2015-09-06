using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NetworkManager : MonoBehaviour {
	
	private const string typeName = "UniqueGameName";
	private const string gameName = "RoomName";  // Name of the server
	private HostData[] hostList;
	public GameObject playerPrefab;
	
	
	// This is to use for our own server incase Unity's master server is in maintenance 
	//MasterServer.ipAddress = "127.0.0.1";
	
	
	// Creates and starts the server for the game
	private void StartServer()
	{
		Network.InitializeServer(4,2500, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}
	
	// Tells us whether the server was successfully started or not
	void OnServerInitalized()
	{
		Debug.Log ("Server Initialized");
	}
	
	private void RefreshHostList()
	{
		MasterServer.RequestHostList(typeName);
	}
	
	void OnMasterServerEvent(MasterServerEvent msEvent)
	{
		if (msEvent == MasterServerEvent.HostListReceived)
			hostList = MasterServer.PollHostList();
	}
	
	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()
	{
		SpawnPlayer();
		Debug.Log("Player spawned");
	}
	
	
	void OnServerInitialized()
	{
		SpawnPlayer();
		Debug.Log("Player spawned");
	}
	
	// Spawn cube at a certain location
	private void SpawnPlayer()
	{
		Network.Instantiate(playerPrefab, new Vector3(0f, 5f, 0f), Quaternion.identity, 0);
	}
	
	
	
	// Displays the buttons GUI interface before start of the game
	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer) {
			if (GUI.Button (new Rect (100, 100, 250, 100), "Start Server"))
				StartServer ();
			
			if (GUI.Button (new Rect (100, 250, 250, 100), "Refresh Hosts"))
				RefreshHostList ();
			
			if (hostList != null) {
				for (int i = 0; i < hostList.Length; i++) {
					if (GUI.Button (new Rect (400, 100 + (110 * i), 300, 100), hostList [i].gameName))
						JoinServer (hostList [i]);
				}
			}
		} else {
			GUILayout.Label ("Connections: " + Network.connections.ToString ());
		}
	}
	
	// The main function the game will go to next.
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {

		
		//Call TwitchVoters to collect votes
		if (Input.GetKey("return")) {
			Debug.Log("Return!");
			TwitchVotes collect = new TwitchVotes();
			Dictionary<string, int> votes = collect.getVotes();
			Debug.Log("End!");
			string temp1 = ""; int temp = -1;
			foreach (KeyValuePair<string, int> pair in votes) {
				Debug.Log (pair.Key + " " + pair.Value);
				if (pair.Value > temp) {
					temp1 = pair.Key;
					temp = pair.Value;
				}
			}
			Destroy(GameObject.Find(temp1));
		}
		
	}
}
