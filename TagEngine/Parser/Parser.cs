using System;
using System.Collections.Generic;
using System.Text;

namespace TagEngine.Parser
{
	/// <summary>
	/// The different types of response the parser can detect
	/// TODO: make this extensible
	/// </summary>
	public enum ParserFlags
	{
		RoomDesc, ItemExam, ItemDesc, PickupItem,
		DropItem, InventoryList, GoRoom, PrintHelp,
		Message, Quit, Look, Talk, Use, GiveItem, Combine
#if DEBUG
		,Debug
#endif
	};

	/// <summary>
	/// Response from the parser
	/// </summary>
	public struct ParserResponse
	{
		#region Fields

		/// <summary>
		/// The flag indicating the type of response
		/// </summary>
		private ParserFlags flag;

		/// <summary>
		/// The tokens created from the input
		/// </summary>
		private Tokeniser tokens;

		/// <summary>
		/// The data
		/// </summary>
		private object data;

		#endregion

		#region Properties

		/// <summary>
		/// The tokens created from the input
		/// </summary>
		public Tokeniser Tokens
		{
			get { return tokens; }
			set { tokens = value; }
		}
		
		/// <summary>
		/// The flag indicating the type of response
		/// </summary>
		public ParserFlags Flag
		{
			get { return flag; }
			set { flag = value; }
		}

		/// <summary>
		/// The data
		/// </summary>
		public object Data
		{
			get { return data; }
			set { data = value; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="tokens">Tokens from the input</param>
		public ParserResponse(Tokeniser tokens)
		{
			this.tokens = tokens;
			this.flag = ParserFlags.Message;
			this.data = null;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="tokens">Tokens from the input</param>
		/// <param name="flag">The flag indicating the type of response</param>
		/// <param name="data">The data</param>
		public ParserResponse(Tokeniser tokens, ParserFlags flag, object data)
		{
			this.tokens = tokens;
			this.flag = flag;
			this.data = data;
		}

		#endregion
	}

	/// <summary>
	/// Provides functions to parse user input
	/// </summary>
	static class Parser
	{
		#region Methods

		/// <summary>
		/// Parse and determine the correct response to user input
		/// </summary>
		/// <param name="input">The input from the user</param>
		/// <returns>The response</returns>
		public static ParserResponse Parse(string input)
		{
			Tokeniser tokens = new Tokeniser(input);

			// check if all words are ignored and thus unusable
			if (tokens.WordCount <= tokens.IgnoreCount)
				return new ParserResponse(tokens, ParserFlags.Message, "I don't understand that.");

			if (tokens.Command.Word == String.Empty)
				return new ParserResponse(tokens, ParserFlags.Message, "You need to tell me what to do.");

			return Parser.ProcessCommand(tokens);
		}

		#endregion

		#region Implementation

		/// <summary>
		/// Determines the correct response from the parser
		/// TODO: convert this to something more generic
		/// </summary>
		/// <returns>The response</returns>
		private static ParserResponse ProcessCommand(Tokeniser tokens)
		{
			string command = tokens.Command.Word; // get recognised command word
			//Debug.WriteLine(command);
			switch (command) // depending on command, respond with appropriate data
			{
				case "go":
				case "walk":
					return new ParserResponse(tokens, ParserFlags.GoRoom, tokens.Direction);

				case "back":
					return new ParserResponse(tokens, ParserFlags.GoRoom, "back");

				case "get":
				case "pick":
				case "collect":
					return new ParserResponse(tokens, ParserFlags.PickupItem, tokens.Unrecognised);

				case "put":
				case "drop":
					return new ParserResponse(tokens, ParserFlags.DropItem, tokens.Unrecognised);

				case "use":
					return new ParserResponse(tokens, ParserFlags.Use, tokens.Unrecognised);

				case "combine":
				case "join":
					return new ParserResponse(tokens, ParserFlags.Combine, tokens.Unrecognised);

				case "give":
				case "pass":
					return new ParserResponse(tokens, ParserFlags.GiveItem, tokens.Unrecognised);

				case "inventory":
				case "inv":
					return new ParserResponse(tokens, ParserFlags.InventoryList, null);

				case "quit":
				case "exit":
					return new ParserResponse(tokens, ParserFlags.Quit, "You're leaving so soon?");

				case "help":
					return new ParserResponse(tokens, ParserFlags.PrintHelp, null);

				case "look":
					return new ParserResponse(tokens, ParserFlags.Look, tokens.Unrecognised);

				case "examine":
				case "inspect":
					return new ParserResponse(tokens, ParserFlags.ItemExam, tokens.Unrecognised);

				case "talk":
				case "ask":
					return new ParserResponse(tokens, ParserFlags.Talk, tokens.Unrecognised);
#if DEBUG
				case "debug":
					return new ParserResponse(tokens, ParserFlags.Debug, tokens.Unrecognised);
#endif

				default:
					return new ParserResponse(tokens, ParserFlags.Message, "I don't understand '" + command + "', but I should.");

			}
		}

		#endregion
	}

}
