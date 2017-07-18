using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEngine.Input.Commands
{
    class Debug : Command
    {
        public Debug() : base("debug", null) { }

        public override Response Process(Engine engine, Tokeniser tokens)
        {
            List<string> subCommands = new List<string> { "variables", "setvariable" };

            string command = null;

            foreach (Token t in tokens)
            {
                if (subCommands.Contains(t.Word))
                {
                    command = t.Word;
                    break;
                }
            }

            switch (command)
            {
                // display list of all variables and their current value
                case "variables":
                    return new Response(engine.GameState.Variables.ToString());

                case "setvariable":
                    return new Response("Not implemented yet.");
            }

            var sb = new StringBuilder();
            sb.Append("Debug commands:" + Environment.NewLine);
            foreach (var c in subCommands)
            {
                sb.Append(" " + c + Environment.NewLine);
            }
            return new Response(sb.ToString());
        }
    }
}
