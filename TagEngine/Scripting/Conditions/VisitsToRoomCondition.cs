using System;
using System.Collections.Generic;
using System.Text;

using TagEngine.Data;

namespace TagEngine.Scripting.Conditions
{
	class VisitsToRoomCondition : Condition
	{
		protected override bool Check(GameState gs)
		{
			return gs.Rooms.ContainsKey((string)Data) && gs.Rooms[(string)Data].NumVisits == (int)Val;
		}
	}
}
