using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Data;

namespace TagEngine.Scripting.Actions
{
    class MessageAction : Action<string, ResponseMessageType>
    {
        public MessageAction(string message, ResponseMessageType type = ResponseMessageType.Normal) : base(message, type)
        {
        }

        public override Response DoAction(GameState gs)
        {
            return new Response(new ResponseMessage(Param1, Param2));
        }
    }
}
