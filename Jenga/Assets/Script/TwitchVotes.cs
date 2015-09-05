using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
public class TwitchVotes : MonoBehaviour {

	private int port;
	string oauth;
	string server;
	private static string[] validVotes = { "up", "down" };

	// Use this for initialization
	void Start () {
		//ThreadStart ts = new ThreadStart(getUserInput);
		//Thread thread = new Thread(ts);
		//thread.Start();
	}
	
	// Update is called once per frame
	List<string> test = new List<string>();
	void Update () {
		Debug.Log("Entering");
		if(Input.GetKey("space")){
			Debug.Log(getUserInput());
		}
		//if time to start vote, start vote
		//else if time is out, close the thread once
	}

	private string getUserInput(){
		port = 6667;
		oauth = "oauth:yhhitgb0bogh4rc1o48s45jo3lks6z";
		server = "irc.twitch.tv";
		System.Net.Sockets.TcpClient sock = new System.Net.Sockets.TcpClient ();
		sock.ReceiveTimeout = 3000;
		sock.Connect (server, port);
		if (!sock.Connected) {
			Debug.Log ("not working");
			
			
		}
		System.IO.TextWriter output;
		System.IO.TextReader input;
		output = new System.IO.StreamWriter (sock.GetStream ());
		input = new System.IO.StreamReader (sock.GetStream ());
		output.Write (
			
			"PASS " + oauth + "\r\n" +
			"NICK " + "Sail338" + "\r\n" +
			"USER " + "Sail338" + "\r\n" +
			"JOIN " 	+ "#sail338" + "" + "\r\n"
			
			
			);
		output.Flush ();
		try {
			for (string rep = input.ReadLine(); ; rep = input.ReadLine()) {
				string[] splitted = rep.Split(':');
				if (splitted.Length > 2) {
					string potentialVote = splitted[2];
					if (Array.Exists(validVotes, vote => vote.Equals(potentialVote)))
						return potentialVote;
				}
			}
		}
		catch {
			return "";
		}
	}
}
