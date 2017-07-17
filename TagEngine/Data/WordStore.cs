using System;
using System.Collections.Generic;
using System.Text;

namespace TagEngine.Data
{
	/// <summary>
	/// Store of words for use in the parser
	/// </summary>
	[Serializable]
	class WordStore
	{
		private static string[] commands = new string[] {
			"go", "quit", "help", "look", "back", "get", "drop", "inventory", "examine", "give", "talk", "use", "combine"
#if DEBUG
			, "debug"
#endif
		};

		private static string[] synonyms = new string[] {
			"walk", "exit", "pick", "put", "inv", "collect", "ask", "inspect", "pass", "join"
		};

		private static string[] ignore = new string[] {
			"the", "a", "an", "is", "of", "on", "for", "by", "at", "what", "when", "why", "how", "do", "from", "who", "down", "up", "where", "some", "to"
		};

		private static string[] directions = new string[] {
			"north", "east", "south", "west"
		};

		/// <summary>
		/// Get the main commands
		/// </summary>
		public static string[] Commands
		{
			get { return commands; }
		}

		/// <summary>
		/// Checks whether a given word is a valid command
		/// </summary>
		/// <param name="word">The word to check</param>
		/// <returns>True if the word is a command</returns>
		public static bool IsCommand(string word)
		{
			// check commands
			foreach (string cmd in commands)
				if (cmd == word) return true;

			// check synonyms
			foreach (string syn in synonyms)
				if (syn == word) return true;

			return false;
		}

		/// <summary>
		/// Checks whether a given word is an ignored word
		/// </summary>
		/// <param name="word">The word to check</param>
		/// <returns>True if the word is ignored</returns>
		public static bool IsIgnored(string word)
		{
			foreach (string ign in ignore)
				if (ign == word) return true;

			return false;
		}

		/// <summary>
		/// Checks whether a given word is a direction
		/// </summary>
		/// <param name="word">The word to check</param>
		/// <returns>True if the word is a direction</returns>
		public static bool IsDirection(string word)
		{
			foreach (string dir in directions)
				if (word == dir) return true;

			// TODO: remove this if possible
			if (word == "back") return true;

			return false;
		}
	}
}
