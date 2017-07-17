using System;
using System.Collections.Generic;
using System.Text;
using TagEngine.Entities;

namespace TagEngine.Scripting.Conditions
{
	class CurrentRoomCondition : Condition
	{
		protected override bool Check(TagEngine.Data.GameState gs)
		{
			return gs.Ego.CurrentRoom == (Room)Val;
		}
	}
}
