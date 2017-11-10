/*
 * MIT License
 * 
 * Copyright (c) 2017 Polarity Studio
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
 * documentation files (the "Software"), to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions
 * of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
 * TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Text;

using TagEngine;
using TagEngine.Data;
using TagEngine.Entities;

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
		int consoleWidth;

		/// <summary>
		/// The height of the console
		/// </summary>
		int consoleHeight;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="T:TagConsole.ConsoleRunner"/> class.
		/// </summary>
		public ConsoleRunner()
		{
			Console.Title = "TagEngine";// engine.GameTitle;
			consoleHeight = Console.BufferHeight;
			consoleWidth = Console.BufferWidth;
		}

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TagConsole.ConsoleRunner"/> class.
        /// </summary>
        /// <param name="args">Arguments.</param>
        public ConsoleRunner(string[] args) : this()
        {
            // TODO: handle command line arguments (maybe find a lib to do it?)
            if (args.Length > 1) { }
        }

		#endregion

		#region Methods

		/// <summary>
		/// Run the game
		/// </summary>
		public void Play()
		{
            // check we have a console
            if (!(Console.In is System.IO.StreamReader || Console.In is System.IO.TextReader)) {
                // if you get here, ensure the project is set to run in an external console
                throw new ApplicationException("Cannot run without a console to work in!");
            }

            // make things happen
            var engine = Engine.Instance;

            // TODO: remove this debug guff
            engine.LoadDebugGame();
            //engine.LoadGame();
            
            WriteLine(engine.GameState.WelcomeMessage);
            WriteLine(engine.GameState.Ego.CurrentRoom.Describe());

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
                    switch (action)
                    {
                        case ResponseAction.Quit:
                        case ResponseAction.LoseGame:
                        case ResponseAction.WinGame:
                            finished = true;
                            break;

                        case ResponseAction.Pause:
                            Pause("Paused. Press enter to continue...");
                            break;

                        case ResponseAction.Dialogue:

                            break;
                    }
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
		string WrapLines(string longText)
		{
            if (consoleWidth == 0) return longText;

			int start = 0; // start of line
			int index = consoleWidth - 1; // end of line
			int backTrack = 0; // how far back from EOL to first space
			StringBuilder sb = new StringBuilder();

			if (longText != null)
			{
				while (index < longText.Length - 1)
				{
					// check if there are any linebreaks in this line
					if (longText.IndexOf("\n", start, index - start, StringComparison.CurrentCulture) == -1)
					{
						backTrack = 0;
						// could use LastIndexOf() but it was having some troubles
						while (longText[index - backTrack] != ' ')
							backTrack++;
					}
					else backTrack = index - longText.IndexOf("\n", start, index - start, StringComparison.CurrentCulture);

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
			ConsoleRunner runner = new ConsoleRunner(args);
			runner.Play();
		}
    }
}
