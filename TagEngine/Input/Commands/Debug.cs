using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEngine.Input.Commands
{
    class Debug : Command
    {
        /// <summary>
        /// List of subcommands the debug command handles
        /// </summary>
        static readonly string[] subCommands = {
                "variables",
                "setvariable",
                "rooms"
            };

        public Debug() : base("debug", null, false) { }

        public override string GetHelpText()
        {
            return "Does things to the insides." + Environment.NewLine + GetDebugCommands();
        }

        public override Response Process(Engine engine, Tokeniser tokens)
        {
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
                        if (subCommands.Contains(command))
                        {
                            r.AddMessage("Debug command >" + command + "< not yet implemented.", ResponseMessageType.Warning);
                        }
                        else
                        {
                            r.AddMessage("Unknown debug command: >" + command + "<", ResponseMessageType.Error);
                        }
                        break;
                }
            }
            
            // show list of available debug subcommands
            r.AddMessage(GetDebugCommands());
            return r;
        }

        string GetDebugCommands()
        {
            var sb = new StringBuilder();
            sb.Append("Debug commands:" + Environment.NewLine);
            foreach (var c in subCommands)
            {
                sb.Append(" " + c + Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}
