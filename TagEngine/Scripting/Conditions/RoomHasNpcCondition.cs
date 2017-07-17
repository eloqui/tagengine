using System;
using System.Collections.Generic;
using System.Text;
using TagEngine.Entities;

namespace TagEngine.Scripting.Conditions
{
	class RoomHasNpcCondition : Condition
	{
		protected override bool Check(TagEngine.Data.GameState gs)
		{
			Room r = (Room)Data;
			Npc npc = (Npc)Val;
			return gs.Npcs[npc.Name].IsInRoom(r);
		}
	}
}
