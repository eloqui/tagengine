using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Data;

namespace TagEngine.Input.Commands
{
    class Help : Command
    {
        public Help() : base("help", null, false) { }

        public override Response Process(Engine engine, Tokeniser tokens)
        {
            if (tokens.WordCount > 1)
            {
                // asking for help with some command
                try
                {
                    var c = CommandManager.GetCommand(tokens.GetTokenAtPosition(1).Word);
                    var text = c.GetHelpText();
                    if (!String.IsNullOrEmpty(text))
                    {
                        return new Response(text);
                    }
                }
                catch (CommandNotFoundException) { }
            }

            var sb = new StringBuilder();

            sb.Append("TODO: game specific help here" + Environment.NewLine);
            
            sb.Append("Available command words are:" + Environment.NewLine);
            
            sb.Append(" ");
            int ii = 0;
            foreach (string cmd in CommandManager.GetPrimaryCommandWords())
            {
                sb.Append(cmd + " ");
                if ((++ii % 6) == 0) sb.Append(Environment.NewLine + " ");
            }

            return new Response(sb.ToString());
        }
    }
}
