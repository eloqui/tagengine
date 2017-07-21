using System;
using System.Collections.Generic;
using System.Text;

using TagEngine.Data;
using TagEngine.Entities;

namespace TagEngine.Scripting.Conditions
{
	class VisitsToRoomCondition : Condition<Room, int>
	{
        public VisitsToRoomCondition(Room room, int visits) : base("visitstoroom", room, visits) { }

        public override bool TestCondition(GameState gs)
		{
            return Param1.NumVisits == Param2;
		}
	}
}
