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
using TagEngine.Input;
using TagEngine.Data;
using TagEngine.Scripting;

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

		}

        #endregion

        #region Methods

        public void LoadDebugGame() // TODO: combine into LoadGame, but don't want to load gamestate until after command/trigger init
        {
            // load commands
            CommandManager.Initialise();

            // load gamestate
            GameState = DataLoader.GetTestGame();
        }

        public void LoadGame() // TODO: load new game from filesystem; saved game from filesystem
        {
            // TODO: load assemblies for this game

            // TODO: reset any other settings/state

            // load commands
            CommandManager.Initialise();

            // TODO: finally, load gamestate
            //GameState = gameState;
        }

		/// <summary>
		/// Process input string, alter the game state as required, and return an appropriate response
		/// </summary>
		/// <param name="input">The input string</param>
		/// <returns>A response</returns>
		public Response ProcessInput(string input)
		{
			// tokenise and parse the input string
			var pr = Parser.Parse(input);

            if (pr.Command == null) return new Response(pr.Message);

            // act upon the input based upon the recognised command
            return pr.Command.Process(this, pr.Tokens);
		}
        
        /// <summary>
        /// Run occurrences matching the trigger
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Response RunOccurrences(ITrigger t)
        {
            var response = new Response();
            foreach (var occurrence in GameState.GetOccurrences(t))
            {
                response.Merge(occurrence.RunActions(GameState));
            }
            return response;
        }

		#endregion
	}
}
