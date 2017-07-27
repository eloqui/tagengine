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
        // TODO: refactor this shit out of here. It was a good idea but it's not working.

        /// <summary>
        /// Prepositions
        /// </summary>
        static readonly string[] prepositions =
        {
            "aboard",
            "about",
            "above",
            "across",
            "after",
            "against",
            "along",
            "amid",
            "among",
            "anti",
            "around",
            "as",
            "at",
            "before",
            "behind",
            "below",
            "beneath",
            "beside",
            "besides",
            "between",
            "beyond",
            "but",
            "by",
            "concerning",
            "considering",
            "despite",
            "down",
            "during",
            "except",
            "excepting",
            "excluding",
            "following",
            "for",
            "from",
            "in",
            "inside",
            "into",
            "like",
            "minus",
            "near",
            "of",
            "off",
            "on",
            "onto",
            "opposite",
            "outside",
            "over",
            "past",
            "per",
            "plus",
            "regarding",
            "round",
            "save",
            "since",
            "than",
            "through",
            "to",
            "toward",
            "towards",
            "under",
            "underneath",
            "unlike",
            "until",
            "up",
            "upon",
            "versus",
            "via",
            "with",
            "within",
            "without",
        };

        /// <summary>
        /// Words that are ignored in the input. (e.g. conjunctions, particles, etc.)
        /// </summary>
		static readonly string[] ignore = {
			"the",
            "a",
            "an",
            "is",
            "what",
            "when",
            "why",
            "how",
            "do",
            "who",
            "where",
            "some",
		};

        /// <summary>
        /// The directions one may travel.
        /// </summary>
        /// <remarks>
        /// Must be in the order: North, South, East, West, Up, Down, Back
        /// </remarks>
        /// <seealso cref="GetDirectionWord(Direction)"/>
		static readonly string[] directions = {
			"north", "south", "east", "west", "up", "down", "back"
		};

		/// <summary>
		/// Checks whether a given word is an ignored word
		/// </summary>
		/// <param name="word">The word to check</param>
		/// <returns>True if the word is ignored</returns>
		public static bool IsIgnored(string word)
        {
            if (String.IsNullOrEmpty(word)) return false;

            return Array.IndexOf(ignore, word) >= 0;
        }

        /// <summary>
        /// Checks if a given word is a preposition
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static bool IsPreposition(string word)
        {
            if (String.IsNullOrEmpty(word)) return false;

            return Array.IndexOf(prepositions, word) >= 0;
        }

		/// <summary>
		/// Checks whether a given word is a direction
		/// </summary>
		/// <param name="word">The word to check</param>
		/// <returns>True if the word is a direction</returns>
		public static bool IsDirection(string word)
        {
            if (String.IsNullOrEmpty(word)) return false;

            return Array.IndexOf(directions, word) >= 0;
        }

        /// <summary>
        /// Get the word for a direction
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        /// <seealso cref="directions"/>
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
                case Direction.Back:  return directions[6];
            }

            // should never get here
            throw new ArgumentOutOfRangeException(nameof(d));
        }

        /// <summary>
        /// Translate a string into a Direction
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static Direction GetDirection(string word)
        {
            if (!IsDirection(word)) throw new ArgumentOutOfRangeException(nameof(word));

            // I18N: have some lookup table in the translation data
            switch (word.ToLower())
            {
                case "north": return Direction.North;
                case "south": return Direction.South;
                case "east":  return Direction.East;
                case "west":  return Direction.West;
                case "up":    return Direction.Up;
                case "down":  return Direction.Down;
                case "back":  return Direction.Back;
            }

            throw new ArgumentOutOfRangeException(nameof(word));
        }
	}
}
