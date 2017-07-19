using System;
using System.Collections.Generic;
using System.Text;

namespace TagEngine.Input
{
//	/// <summary>
//	/// The different types of response the parser can detect
//	/// TODO: make this extensible
//	/// </summary>
//	public enum ParserFlags
//	{
//		RoomDesc, ItemExam, ItemDesc, PickupItem,
//		DropItem, InventoryList, GoRoom, PrintHelp,
//		Message, Quit, Look, Talk, Use, GiveItem, Combine
//#if DEBUG
//		,Debug
//#endif
//	};

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
		/// The data
		/// </summary>
		//public object Data { get; private set; }

        public Command Command { get; private set; }

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
		public ParserResponse(Tokeniser tokens, Command command = null, ResponseMessage message = new ResponseMessage())
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
            Command command;
            ResponseMessage message;
            try
            {
                command = CommandManager.GetCommand(t.Command.Word);
                message = new ResponseMessage();

            } catch (CommandNotFoundException cnfe)
            {
                command = null;
                message.Message = cnfe.Message;
                message.Type = ResponseMessageType.Error;
            }
            
            // pass it back
            return new ParserResponse(t, command, message);
        }

        #endregion
    }

}
