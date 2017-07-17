using System;
using System.Collections.Generic;
using System.Text;

namespace TagEngine
{
	/// <summary>
	/// The response structure stores the game engine's response to a user command
	/// </summary>
	public struct Response
	{
		#region Fields

		/// <summary>
		/// Whether to quit the game
		/// </summary>
		private bool quit;

		/// <summary>
		/// A message to output
		/// </summary>
		private string message;

		/// <summary>
		/// Whether the message is important
		/// </summary>
		private bool isImportant;

		#endregion

		#region Properties

		/// <summary>
		/// Whether to quit the game
		/// </summary>
		public bool Quit
		{
			get { return quit; }
		}

		/// <summary>
		/// A message to output
		/// </summary>
		public string Message
		{
			get { return message; }
		}

		/// <summary>
		/// Whether the message is important
		/// </summary>
		public bool IsImportant
		{
			get { return isImportant; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">The message to output</param>
		public Response(string message)
		{
			this.message = message;
			this.quit = false;
			this.isImportant = false;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">The message to output</param>
		/// <param name="isImportant">Whether the message is important</param>
		public Response(string message, bool isImportant)
		{
			this.message = message;
			this.quit = false;
			this.isImportant = isImportant;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="message">The message to output</param>
		/// <param name="quit">Whether to quit the game</param>
		/// <param name="isImportant">Whether the message is important</param>
		public Response(string message, bool quit, bool isImportant)
		{
			this.message = message;
			this.quit = quit;
			this.isImportant = isImportant;
		}

		#endregion
	}
}
