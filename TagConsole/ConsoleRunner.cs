using System;
using System.Text;

using TagEngine;
using TagEngine.Data;

namespace TagConsole
{
	/// <summary>
	/// The runner class is a Console-based frontend to the TagEngine
	/// </summary>
	class ConsoleRunner
	{
		#region Fields

		/// <summary>
		/// The width of the console
		/// </summary>
		private int consoleWidth;

		/// <summary>
		/// The height of the console
		/// </summary>
		private int consoleHeight;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public ConsoleRunner()
		{
			Console.Title = "TagEngine";// engine.GameTitle;
			this.consoleHeight = Console.BufferHeight;
			this.consoleWidth = Console.BufferWidth;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Run the game
		/// </summary>
		public void Play()
		{
            var engine = Engine.Instance;

            // TODO: remove this debug guff
            engine.LoadGame(DataLoader.GetTestGame());
            
            WriteLine(engine.GameState.WelcomeMessage);
			//WriteLine(engine.Describe(engine.CurrentRoom));

			bool finished = false;
			while (!finished)
			{
				string input = null;
				Console.Write("> ");
				input = Console.ReadLine();

				// process input
				Response response = engine.ProcessInput(input);

                // handle response messages
                foreach (var message in response.Messages)
                {
                    // handle message.Type
                    switch (message.Type)
                    {
                        case ResponseMessageType.Error:
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case ResponseMessageType.Warning:
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case ResponseMessageType.Important:
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                    }

                    WriteLine(message.Message);

                    Console.ResetColor();
                }

                // handle response actions
                foreach (var action in response.Actions)
                {
                    if (action == ResponseAction.Quit) finished = true;
                    if (action == ResponseAction.Pause) Pause("Paused. Press enter to continue...");
                }
			}

			WriteLine("Thank you for playing. Goodbye!");
			Pause("Press enter to close...");
		}

		/// <summary>
		/// Pause the game
		/// </summary>
		/// <param name="instructions">Optional instructions to output before pausing</param>
		public void Pause(string instructions)
		{
			if (!String.IsNullOrEmpty(instructions)) Write(instructions);
			Console.ReadLine();
		}

		/// <summary>
		/// Writes the specified string to the output
		/// </summary>
		/// <param name="line">The text</param>
		public void Write(string line)
		{
			Console.Write(this.WrapLines(line));
		}

		/// <summary>
		/// Writes the specified string to the output followed by a line separator character
		/// </summary>
		/// <param name="line">The text</param>
		public void WriteLine(string line)
		{
			Console.WriteLine(this.WrapLines(line));
		}

		#endregion

		#region Implementation

		/// <summary>
		/// Wrap text lines at a certain length
		/// </summary>
		/// <param name="longText"></param>
		/// <returns></returns>
		private string WrapLines(string longText)
		{
			int start = 0; // start of line
			int index = this.consoleWidth - 1; // end of line
			int backTrack = 0; // how far back from EOL to first space
			StringBuilder sb = new StringBuilder();

			if (longText != null)
			{
				while (index < longText.Length - 1)
				{
					// check if there are any linebreaks in this line
					if (longText.IndexOf("\n", start, index - start) == -1)
					{
						backTrack = 0;
						// could use LastIndexOf() but it was having some troubles
						while (longText[index - backTrack] != ' ')
							backTrack++;
					}
					else backTrack = index - longText.IndexOf("\n", start, index - start);

					// append the appropriate length to the string
					sb.Append(longText.Substring(start, (index - backTrack) - start) + "\n");

					// move forward
					start = index - backTrack + 1;
					index = start + this.consoleWidth - 1;
				}

				// append what's left over
				sb.Append(longText.Substring(start) + "\n");
			}

			return sb.ToString();
		}

		#endregion

		/// <summary>
		/// The Main entry point method
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			ConsoleRunner runner = new ConsoleRunner();
			runner.Play();
		}
	}
}
