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
	public static class WordStore
	{
//		private static string[] commands = {
//			"go", "quit", "help", "look", "back", "get", "drop", "inventory", "examine", "give", "talk", "use", "combine"
//#if DEBUG
//			, "debug"
//#endif
//		};

		//private static string[] synonyms = {
		//	"walk", "exit", "pick", "put", "inv", "collect", "ask", "inspect", "pass", "join"
		//};

		private static string[] ignore = {
			"the", "a", "an", "is", "of", "on", "for", "by", "at", "what", "when", "why", "how", "do", "from", "who", "where", "some", "to", "with"
		};

		private static string[] directions = {
			"north", "east", "south", "west", "up", "down", "back"
		};
                
		/// <summary>
		/// Checks whether a given word is a valid command
		/// </summary>
		/// <param name="word">The word to check</param>
		/// <returns>True if the word is a command</returns>
		//public bool IsCommand(string word)
		//{
		//	// check commands
		//	foreach (string cmd in Commands)
		//		if (cmd == word) return true;

		//	return false;
		//}

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

			return false;
		}

        /// <summary>
        /// Get the word for a direction
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string GetDirectionWord(Direction d)
        {
            switch (d)
            {
                case Direction.North: return directions[0];
                case Direction.South: return directions[1];
                case Direction.East:  return directions[2];
                case Direction.West:  return directions[3];
                case Direction.Up:    return directions[4];
                case Direction.Down:  return directions[5];
            }

            // should never get here
            throw new ArgumentOutOfRangeException("Not a valid direction");
        }

        /// <summary>
        /// Translate a string into a Direction
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static Direction GetDirection(string word)
        {
            if (!IsDirection(word)) throw new ArgumentOutOfRangeException(word + " is not a valid direction");

            // I18N: have some lookup table in the translation data
            switch (word.ToLower())
            {
                case "north": return Direction.North;
                case "south": return Direction.South;
                case "east":  return Direction.East;
                case "west":  return Direction.West;
                case "up":    return Direction.Up;
                case "down":  return Direction.Down;
             }

            throw new ArgumentOutOfRangeException(word + " is not a known direction");
        }
	}
}
