using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Data;
using TagEngine.Entities;

namespace TagEngine.Scripting.Actions
{
    class MoveNpcAction : Action<Npc, Room>
    {
        public MoveNpcAction(Npc npc, Room room) : base(npc, room) { }

        public override Response DoAction(GameState gs)
        {
            if (Param1.CurrentRoom != Param2) Param1.MoveTo(Param2);

            return null;
        }
    }
}
