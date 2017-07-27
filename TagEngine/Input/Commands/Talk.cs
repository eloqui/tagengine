using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Entities;
using TagEngine.Scripting;

namespace TagEngine.Input.Commands
{
    class Talk : Command
    {
        public Talk() : base("talk", new List<string> { "ask" }) { }

        /// <summary>
        /// Trigger for a talk command
        /// </summary>
        public class Trigger : Trigger<Npc>
        {
            public Trigger(Npc npc) : base("talk", npc) { }
        }

        protected override Response ProcessInternal(Engine engine, Tokeniser tokens)
        {
            throw new NotImplementedException();
        }
    }
}
