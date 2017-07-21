using System;
using System.Collections.Generic;
using System.Text;
using TagEngine.Data;
using TagEngine.Entities;

namespace TagEngine.Scripting.Conditions
{
	class RoomHasNpcCondition : Condition<Room, Npc>
	{
        public RoomHasNpcCondition(Room room, Npc npc) : base("roomhasnpc", room, npc) { }

        public override bool TestCondition(GameState gs)
        {
            return Param1.HasNpc(Param2);
		}
    }
}
