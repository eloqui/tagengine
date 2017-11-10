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
using System.Collections.Generic;

namespace TagEngine
{
    /// <summary>
    /// A suggestion to the Client on actions to take external to the Engine
    /// </summary>
    public enum ResponseAction
    {
        None,
        Dialogue,
        Quit,
        Pause,
        LoseGame,
        WinGame
    }
    /// <summary>
    /// The type is an indication to the Client of how a message might be displayed/handled
    /// </summary>
    public enum ResponseMessageType
    {
        Normal,
        Important,
        Warning,
        Error
    }

    /// <summary>
    /// A message
    /// </summary>
    public class ResponseMessage
    {
        /// <summary>
        /// The message to display in the Client
        /// </summary>
        public string Message; // TODO: add some sort of markup for colours/etc.

        /// <summary>
        /// The type is an indication to the Client of how this message might be displayed/handled
        /// </summary>
        public ResponseMessageType Type;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type"></param>
        public ResponseMessage(string message, ResponseMessageType type = ResponseMessageType.Normal)
        {
            Message = message;
            Type = type;
        }
    }
    
	/// <summary>
	/// The response structure stores the game engine's response to a user command
	/// </summary>
	public class Response
	{
        #region Fields & Properties
        
        /// <summary>
        /// A message to output
        /// </summary>
        public List<ResponseMessage> Messages { get; protected set; }

        /// <summary>
        /// Action(s) to take after this response
        /// </summary>
        public List<ResponseAction> Actions { get; protected set; }

        /// <summary>
        /// Whether this response is empty
        /// </summary>
        public bool Empty
        {
            get
            {
                return !HasMessage && !HasAction;
            }
        }

        /// <summary>
        /// Whether this response has a message
        /// </summary>
        public bool HasMessage
        {
            get
            {
                return Messages.Count > 0;
            }
        }

        /// <summary>
        /// Whether this response has an action
        /// </summary>
        public bool HasAction
        {
            get
            {
                return Actions.Count > 0;
            }
        }

		#endregion

		#region Methods
        
		/// <summary>
		/// Constructor
		/// </summary>
		public Response()
		{
            Messages = new List<ResponseMessage>();
            Actions = new List<ResponseAction>();
		}

        /// <summary>
        /// Construct a response with a normal message
        /// </summary>
        /// <param name="message"></param>
        public Response(string message)
            : this()
        {
            AddMessage(message);
        }

        /// <summary>
        /// Construct a response with a response message
        /// </summary>
        /// <param name="message"></param>
        public Response(ResponseMessage message)
            : this()
        {
            AddMessage(message);
        }

        /// <summary>
        /// Construct a response with a normal message and an action
        /// </summary>
        /// <param name="message"></param>
        /// <param name="action"></param>
        public Response(string message, ResponseAction action)
            : this(message)
        {
            AddAction(action);
        }

        /// <summary>
        /// Merge another response into this one
        /// </summary>
        /// <param name="r"></param>
        public void Merge(Response r)
        {
            if (r == null) return;

            Messages.AddRange(r.Messages);
            Actions.AddRange(r.Actions);
        }

        /// <summary>
        /// Add an action to the response
        /// </summary>
        /// <remarks>
        /// The action is a suggestion to the Client to take some action external to the engine, such as quitting the program.
        /// </remarks>
        /// <param name="action"></param>
        public void AddAction(ResponseAction action)
        {
            Actions.Add(action);
        }

        /// <summary>
        /// Add a message to the response
        /// </summary>
        /// <param name="rm"></param>
        public void AddMessage(ResponseMessage rm)
        {
            if (rm == null) return;

            Messages.Add(rm);
        }

        /// <summary>
        /// Add a message to the response
        /// </summary>
        /// <param name="message"></param>
        /// <param name="type">The type is an indication to the Client of how this message might be displayed/handled</param>
        public void AddMessage(string message, ResponseMessageType type = ResponseMessageType.Normal)
        {
            AddMessage(new ResponseMessage(message, type));
        }

		#endregion
	}
}
