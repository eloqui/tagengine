using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEngine.Input.Commands
{
    class Quit : Command
    {
        public Quit() : base("quit", new List<string> { "exit", "\\q", "bye" }, false) { }

        protected override Response ProcessInternal(Engine engine, Tokeniser tokens)
        {
            return new Response("You're leaving so soon?", ResponseAction.Quit);
        }
    }
}
