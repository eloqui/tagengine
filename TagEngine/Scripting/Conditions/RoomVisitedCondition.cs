using System;
using System.Collections.Generic;
using System.Text;

using TagEngine.Data;
using TagEngine.Entities;

namespace TagEngine.Scripting.Conditions
{
	class RoomVisitedCondition : Condition<Room, bool>
	{
        public RoomVisitedCondition(Room room, bool hasVisited) : base("roomvisited", room, hasVisited) { }

        public override bool TestCondition(GameState gs)
        {
            var room = Param1 == null ? gs.Ego.CurrentRoom : Param1;
            
            if (Param2 == false)
            {
                return !room.HasVisited;
            }
            else
            {
                return room.HasVisited;
            }
        }
	}
}
