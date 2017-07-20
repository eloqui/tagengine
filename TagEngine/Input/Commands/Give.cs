using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEngine.Input.Commands
{
    class Give : Command
    {
        public Give() : base("give", new List<string> { "pass" }) { }

        public override Response Process(Engine engine, Tokeniser tokens)
        {
            throw new NotImplementedException();
        }
    }
}
