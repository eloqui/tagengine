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
        public Help() : base("help", null) { }

        public override Response Process(Engine engine, Tokeniser tokens)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("TODO: game specific help here");
            sb.Append(Environment.NewLine);
            sb.Append("Available command words are:" + Environment.NewLine);

            var ws = new WordStore();

            sb.Append(" ");
            int ii = 0;
            foreach (string cmd in ws.Commands)
            {
                sb.Append(cmd + " ");
                if ((++ii % 6) == 0) sb.Append(Environment.NewLine + " ");
            }

            return new Response(sb.ToString());
        }
    }
}
