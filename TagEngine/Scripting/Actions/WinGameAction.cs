using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Data;

namespace TagEngine.Scripting.Actions
{
    class WinGameAction : Action<string>
    {
        public WinGameAction(string message) : base(message) { }

        public override Response DoAction(GameState gs)
        {
            string message;
            if (String.IsNullOrWhiteSpace(Param1))
            {
                message = "Congratulations, you won the game!";
            }
            else
            {
                message = Param1;
            }
            return new Response(message, ResponseAction.WinGame);
        }
    }
}
