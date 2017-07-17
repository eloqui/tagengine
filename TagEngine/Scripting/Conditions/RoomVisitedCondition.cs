using System;
using System.Collections.Generic;
using System.Text;

using TagEngine.Data;

namespace TagEngine.Scripting.Conditions
{
	class RoomVisitedCondition : Condition
	{
		protected override bool Check(GameState gs)
		{
			if ((bool)Val == false)
				return !gs.Ego.CurrentRoom.HasVisited;
			else
				return gs.Ego.CurrentRoom.HasVisited;
		}
	}
}
