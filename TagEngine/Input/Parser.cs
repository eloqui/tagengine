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
using System.Text;

namespace TagEngine.Input
{
	/// <summary>
	/// Response from the parser
	/// </summary>
	public struct ParserResponse
	{
		#region Properties

		/// <summary>
		/// The tokens created from the input
		/// </summary>
		public Tokeniser Tokens { get; private set; }
        
        /// <summary>
        /// The command
        /// </summary>
        public ICommand Command { get; private set; }

        /// <summary>
        /// A message to pass back
        /// </summary>
        public ResponseMessage Message { get; private set; }

		#endregion

		#region Constructors
        
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="tokens">Tokens from the input</param>
		/// <param name="command"></param>
        /// <param name="message"></param>
		public ParserResponse(Tokeniser tokens, ICommand command = null, ResponseMessage message = null)
		{
            Tokens = tokens;
            Command = command;
            Message = message;
		}

		#endregion
	}

	/// <summary>
	/// Provides functions to parse user input
	/// </summary>
	static class Parser
	{
		#region Methods

        public static ParserResponse Parse(string input)
        {
            var t = new Tokeniser(input);

            // check if all words are ignored and thus unusable
            if (t.WordCount <= t.IgnoreCount) return new ParserResponse(t, message: new ResponseMessage("I don't understand that.", ResponseMessageType.Warning));

            // check if the input has no command word
            if (String.IsNullOrEmpty(t.Command.Word)) return new ParserResponse(t, message: new ResponseMessage("You need to tell me what to do.", ResponseMessageType.Warning));

            // find a Command that matches the command word from the token
            ICommand command;
            ResponseMessage message;
            try
            {
                command = CommandManager.GetCommand(t.Command.Word);
                message = null;

            } catch (CommandNotFoundException cnfe)
            {
                command = null;
                message = new ResponseMessage(cnfe.Message, ResponseMessageType.Error);
            }
            
            // pass it back
            return new ParserResponse(t, command, message);
        }

        #endregion
    }

}
