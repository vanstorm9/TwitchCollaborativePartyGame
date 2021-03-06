﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
public class TwitchVotes : MonoBehaviour {

	private int port;
	string oauth;
	string server;
	private static string[] validVotes = { "a1", "a2" };
	int gameTurn = 0, cooldown = 5;
	bool timeUp = true;

	// Use this for initialization
	void Start() {
		//ThreadStart ts = new ThreadStart(getUserInput);
		//Thread thread = new Thread(ts);
		//thread.Start();
	}

	// Update is called once per frame
	void Update() {

	}

	public void clearVotes() {
		voteList.Clear();
	}

	Dictionary<string, int> voteList = new Dictionary<string, int>();
	public Dictionary<string, int> getVotes() {
		float timeStamp = Time.time + cooldown;
		while (timeStamp <= Time.time) { 
			string vote = getUserInput();
			if (!(vote.Equals("Timed out")) && !(voteList.ContainsKey(vote))) {
				voteList.Add(vote, 1);
			}
			else if (voteList.ContainsKey(vote))
				voteList[vote] += 1;
		}

		return voteList;
	}


	public string getUserInput() {
		port = 6667;
		oauth = "oauth:yhhitgb0bogh4rc1o48s45jo3lks6z";
		server = "irc.twitch.tv";
		System.Net.Sockets.TcpClient sock = new System.Net.Sockets.TcpClient();
		sock.ReceiveTimeout = cooldown * 1000;
		sock.Connect(server, port);
		if (!sock.Connected) {
			Debug.Log("not working");


		}
		System.IO.TextWriter output;
		System.IO.TextReader input;
		output = new System.IO.StreamWriter(sock.GetStream());
		input = new System.IO.StreamReader(sock.GetStream());
		output.Write(

			"PASS " + oauth + "\r\n" +
			"NICK " + "Sail338" + "\r\n" +
			"USER " + "Sail338" + "\r\n" +
			"JOIN " + "#sail338" + "" + "\r\n"


			);
		output.Flush();
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
			return "Timed out";
		}
	}
}
