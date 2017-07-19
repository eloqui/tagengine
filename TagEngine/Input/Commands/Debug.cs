using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEngine.Input.Commands
{
    class Debug : Command
    {
        public Debug() : base("debug", null, false) { }

        public override Response Process(Engine engine, Tokeniser tokens)
        {
            List<string> subCommands = new List<string> {
                "variables",
                "setvariable"
            };

            Response r = new Response();

            if (tokens.WordCount > 1)
            {
                string command = tokens.GetTokenAtPosition(1).Word;

                switch (command)
                {
                    // display list of all variables and their current value
                    case "variables":
                        r.AddMessage(engine.GameState.Variables.ToString());
                        return r;
                        
                    // set a variable to a value
                    case "setvariable":
                        if (tokens.WordCount < 4)
                        {
                            r.AddMessage("debug setvariable [variable] [value]", ResponseMessageType.Warning);
                        }
                        else
                        {
                            string variableName = tokens.GetTokenAtPosition(2).Word;
                            string value = tokens.GetTokenAtPosition(3).Word; // TODO: cast to type??
                            engine.GameState.Variables.Set(variableName, value);
                            return r;
                        }
                        break;

                    // an unknown debug command
                    default:
                        r.AddMessage("Unknown debug command: " + command, ResponseMessageType.Error);
                        break;
                }

                
            }
            
            var sb = new StringBuilder();
            sb.Append("Debug commands:" + Environment.NewLine);
            foreach (var c in subCommands)
            {
                sb.Append(" " + c + Environment.NewLine);
            }
            r.AddMessage(sb.ToString());

            return r;
        }
    }
}
