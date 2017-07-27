using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Data;

namespace TagEngine.Scripting.Actions
{
    class LoseGameAction : Action<string>
    {
        public LoseGameAction(string message) : base(message) { }

        public override Response DoAction(GameState gs)
        {
            string message;
            if (String.IsNullOrWhiteSpace(Param1))
            {
                message = "Sorry, but you lost the game!";
            } else
            {
                message = Param1;
            }
            return new Response(message, ResponseAction.LoseGame);
        }
    }
}
