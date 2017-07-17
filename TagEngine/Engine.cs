using System;
using System.Collections.Generic;
using System.Text;

using TagEngine.Entities;
using TagEngine.Parser;
using TagEngine.Data;

namespace TagEngine
{
	/// <summary>
	/// The Engine class is responsible for setting up a game instance
	/// and acting upon user input.
	/// </summary>
	public class Engine
	{
		#region Fields

		/// <summary>
		/// The current game state
		/// </summary>
		private GameState gs;

		/// <summary>
		/// The single instance of this class
		/// </summary>
		static readonly Engine instance = new Engine();

		#endregion

		#region Properties

		/// <summary>
		/// Get the GameState interface
		/// </summary>
		public GameState GameState
		{
			get { return gs; }
		}

		public string Welcome
		{
			get { return "Welcome!"; }
		}

		public Room CurrentRoom
		{
			get { return CurrentRoom; }
		}

		/// <summary>
		/// Get the single instance of this class
		/// </summary>
		public static Engine Instance
		{
			get { return instance; }
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Private Constructor
		/// </summary>
		private Engine()
		{
			this.gs = new GameState();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Process input string, alter the game state as required, and return an appropriate response
		/// </summary>
		/// <param name="input">The input string</param>
		/// <returns>A response</returns>
		public Response ProcessInput(string input)
		{
			// tokenise and parse the input string
			ParserResponse pr = Parser.Parser.Parse(input);

			string message = "";
			bool important = false;
			bool quit = false;

			// act upon the input based upon the recognised command
			// TODO: change to delegates and events
			switch (pr.Flag)
			{
#if DEBUG
				case ParserFlags.Debug: // debugging call
					message = this.DebugHandler(pr.Tokens.Unrecognised);
					important = true;
					break;
#endif

				case ParserFlags.Message: // simple message sent directly to user
					message = (string)pr.Data;
					important = true;
					break;

				case ParserFlags.PrintHelp: // user wants some help
					message = Help();
					important = true;
					break;

				case ParserFlags.Quit: // quit the game please
					message = (string)pr.Data;
					quit = true;
					break;

				default: // woops, don't know what happened here...
					if (pr.Data != null)
						message = pr.Data.ToString();
					else message = "Woops";
					important = true;
					break;
			}

			return new Response(message, quit, important);
		}

		#endregion

		#region Implementation

		/// <summary>
		/// Handle the debug call
		/// </summary>
		/// <param name="tokens"></param>
		/// <returns></returns>
		private string DebugHandler(List<Token> tokens)
		{
			List<string> commands = new List<string>();
			commands.AddRange(new string[] { "variables", "setVariable" });

			string command = null;

			foreach (Token t in tokens)
			{
				if (commands.Contains(t.Word)) {
					command = t.Word;
					break;
				}
			}

			switch (command)
			{
				// display list of all variables and their current value
				case "variables":
					return this.GameState.Variables.ToString();

				case "setVariable":
					return "Not implemented yet.";
			}

			return "Unknown command!";
		}

		/// <summary>
		/// Get a help message and command list
		/// </summary>
		/// <returns>The help message</returns>
		private string Help()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("TODO: game specific help here");
			sb.Append(Environment.NewLine);
			sb.Append("Available command words are:" + Environment.NewLine);
			sb.Append(GetCommandList());
			return sb.ToString();
		}

		/// <summary>
		/// Get list of available commands
		/// </summary>
		/// <returns>Commands</returns>
		private string GetCommandList()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(" ");
			int ii = 0;
			foreach (string cmd in WordStore.Commands)
			{
				sb.Append(cmd + " ");
				if ((++ii % 6) == 0) sb.Append(Environment.NewLine + " ");
			}
			return sb.ToString();
		}

		#endregion
	}
}
