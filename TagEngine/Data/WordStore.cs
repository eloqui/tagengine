using System;
using System.Collections.Generic;
using System.Text;
using TagEngine.Entities;
using TagEngine.Input;

namespace TagEngine.Data
{
	/// <summary>
	/// Store of words for use in the parser
	/// </summary>
	public class WordStore
	{
//		private static string[] commands = {
//			"go", "quit", "help", "look", "back", "get", "drop", "inventory", "examine", "give", "talk", "use", "combine"
//#if DEBUG
//			, "debug"
//#endif
//		};

		//private string[] synonyms = {
		//	"walk", "exit", "pick", "put", "inv", "collect", "ask", "inspect", "pass", "join"
		//};

		private string[] ignore = {
			"the", "a", "an", "is", "of", "on", "for", "by", "at", "what", "when", "why", "how", "do", "from", "who", "where", "some", "to", "with"
		};

		private string[] directions = {
			"north", "east", "south", "west", "up", "down", "back"
		};

		/// <summary>
		/// Get the main commands
		/// </summary>
		public string[] Commands { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public WordStore()
        {
            // load in words
            Commands = CommandManager.GetCommandWords();
        }

		/// <summary>
		/// Checks whether a given word is a valid command
		/// </summary>
		/// <param name="word">The word to check</param>
		/// <returns>True if the word is a command</returns>
		public bool IsCommand(string word)
		{
			// check commands
			foreach (string cmd in Commands)
				if (cmd == word) return true;

			return false;
		}

		/// <summary>
		/// Checks whether a given word is an ignored word
		/// </summary>
		/// <param name="word">The word to check</param>
		/// <returns>True if the word is ignored</returns>
		public bool IsIgnored(string word)
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
		public bool IsDirection(string word)
		{
			foreach (string dir in directions)
				if (word == dir) return true;

			return false;
		}
	}
}
