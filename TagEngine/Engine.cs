using System;
using System.Collections.Generic;
using System.Text;

using TagEngine.Entities;
using TagEngine.Input;
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
		/// The single instance of this class
		/// </summary>
		private static Engine instance;

		#endregion

		#region Properties

		/// <summary>
		/// The current GameState
		/// </summary>
		public GameState GameState { get; protected set; }
        
		/// <summary>
		/// Get the single instance of this class
		/// </summary>
		public static Engine Instance
		{
			get {
                if (instance == null)
                {
                    instance = new Engine();
                }
                return instance;
            }
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Private Constructor
		/// </summary>
		private Engine()
		{
			LoadGame(new GameState());
		}
        
        #endregion

        #region Methods

        public void LoadGame(GameState gameState)
        {
            // TODO: reset any other settings/state
            GameState = gameState;
        }

		/// <summary>
		/// Process input string, alter the game state as required, and return an appropriate response
		/// </summary>
		/// <param name="input">The input string</param>
		/// <returns>A response</returns>
		public Response ProcessInput(string input)
		{
			// tokenise and parse the input string
			ParserResponse pr = Input.Parser.Parse(input);
            
            if (pr.Command == null) return new Response(pr.Message);

            // act upon the input based upon the recognised command
            var response = pr.Command.Process(this, pr.Tokens);

            return response;
		}

		#endregion

		#region Implementation
        
		#endregion
	}
}
