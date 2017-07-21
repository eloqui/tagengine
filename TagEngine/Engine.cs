using System;
using System.Collections.Generic;
using System.Text;

using TagEngine.Entities;
using TagEngine.Input;
using TagEngine.Data;

namespace TagEngine
{
    /// <summary>
    /// Directions of movement
    /// </summary>
    public enum Direction
    {
        North,
        South,
        East,
        West,
        Up,
        Down,
        Back
    }

    /// <summary>
    /// Extension functions for the Direction enum
    /// </summary>
    public static class DirectionExtensions
    {
        /// <summary>
        /// Get the direction string for a Direction
        /// </summary>
        /// <param name="d">The direction</param>
        /// <returns>The direction string</returns>
        public static string GetExitDirection(this Direction d)
        {
            return WordStore.GetDirectionWord(d);
        }
    }

    /// <summary>
    /// The Engine class is responsible for setting up a game instance
    /// and acting upon user input.
    /// </summary>
    public class Engine
	{
        #region Singleton

        /// <summary>
        /// The single instance of this class
        /// </summary>
        static Engine instance;

        /// <summary>
        /// Get the single instance of this class
        /// </summary>
        public static Engine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Engine();
                }
                return instance;
            }
        }

        #endregion

        #region Fields & Properties

        /// <summary>
        /// The current GameState
        /// </summary>
        public GameState GameState { get; protected set; }
        
		#endregion

		#region Constructor

		/// <summary>
		/// Private Constructor
		/// </summary>
		Engine()
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

            Response response;

            if (pr.Command == null)
            {
                response = new Response();
                response.AddMessage(pr.Message);
            }
            else
            {
                // act upon the input based upon the recognised command
                response = pr.Command.Process(this, pr.Tokens);
            }

            return response;
		}

        /// <summary>
        /// Describe an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string Describe(ILookable item)
        {
            return item.Description;
        }

        /// <summary>
        /// Examine an entity
        /// </summary>
        /// <param name="item"></param>
        /// <param name="describeIfEmpty">If true, will return the short description if there is no extended description</param>
        /// <returns></returns>
        public string Examine(ILookable item, bool describeIfEmpty = false)
        {
            if (describeIfEmpty && String.IsNullOrWhiteSpace(item.ExtendedDescription)) return item.Description;

            return item.ExtendedDescription;
        }

		#endregion

		#region Implementation
        
		#endregion
	}
}
