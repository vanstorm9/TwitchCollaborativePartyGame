using System;

namespace TwitchHandler
{
	class MainClass
	{
		private static string[] validVotes = {"up","down"};

		public static void Main (string[] args)
		{
			int port = 6667;
			String nick = "Sail338";
			String oath = "oauth:yhhitgb0bogh4rc1o48s45jo3lks6z";

			String server = "irc.twitch.tv";
			while (true) {
				Console.WriteLine (connect (server, port, oath));
			}
	}

		public static string connect(String server,int port,String ouath){

		
			System.Net.Sockets.TcpClient sock = new System.Net.Sockets.TcpClient ();
			sock.Connect (server, port);
			if (!sock.Connected) {
				Console.Write ("not working hoe");


			}
			System.IO.TextWriter output;
			System.IO.TextReader input;
			output = new System.IO.StreamWriter (sock.GetStream ());
			input = new System.IO.StreamReader (sock.GetStream ());
			output.Write (

				"PASS " + ouath + "\r\n" +
				"NICK " + "Sail338" + "\r\n" +
				"USER " + "Sail338" + "\r\n" +
				"JOIN " 	+ "#sail338" + "" + "\r\n"

			
			);
			output.Flush ();

			for (String rep = input.ReadLine ();; rep = input.ReadLine ()) {
				string[] splitted = rep.Split (':');
				if (splitted.Length > 2) {
					string potentialVote = splitted [2];
					if (Array.Exists (validVotes, vote => vote.Equals(potentialVote)))
						return potentialVote;
				}
			}
		}
	}}