using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
public class TwitchVotes  {

	private int port;
	string oauth;
	string server;
	private static string[] validVotes;
	int gameTurn = 0, cooldown = 3;
	bool timeUp = true;

	public TwitchVotes() { 
		string[] names = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J"};
		List<string> tempList = new List<string>();
		foreach(string letter in names){
			string tempName = letter;
			for(int i = 1; i <= 3; i++){
				tempName = tempName + i;
				tempList.Add(tempName);
			}
		}
		validVotes = tempList.ToArray();
	}

	Dictionary<string, int> voteList = new Dictionary<string, int>();
	public Dictionary<string, int> getVotes() {
		for(int i = 0; i < 3; i++){
			Debug.Log(Time.time);
			string vote = getUserInput();;
			if (!(voteList.ContainsKey(vote)) && Array.Exists(validVotes, test => test.Equals(vote))) {
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
		for (string rep = input.ReadLine(); ; rep = input.ReadLine()) {
			string[] splitted = rep.Split(':');
			if (splitted.Length > 2) {					string potentialVote = splitted[2];
				if (Array.Exists(validVotes, vote => vote.Equals(potentialVote))){
					sock.Close();
					return potentialVote;
				}
			}
		}
	}
}
