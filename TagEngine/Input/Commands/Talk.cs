using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEngine.Input.Commands
{
    class Talk : Command
    {
        public Talk() : base("talk", new List<string> { "ask" }) { }

        public override Response Process(Engine engine, Tokeniser tokens)
        {
            throw new NotImplementedException();
        }
    }
}
