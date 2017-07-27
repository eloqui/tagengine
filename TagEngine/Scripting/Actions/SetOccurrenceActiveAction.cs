using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Data;

namespace TagEngine.Scripting.Actions
{
    class SetOccurrenceActiveAction : Action<Occurrence, bool>
    {
        public SetOccurrenceActiveAction(Occurrence occurrence, bool active = true) : base(occurrence, active) { }

        public override Response DoAction(GameState gs)
        {
            Param1.IsActive = Param2;

            return null;
        }
    }
}
