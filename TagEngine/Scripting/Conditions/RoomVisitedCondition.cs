using System;
using System.Collections.Generic;
using System.Text;

using TagEngine.Data;
using TagEngine.Entities;

namespace TagEngine.Scripting.Conditions
{
	class RoomVisitedCondition : Condition<Room, bool>
	{
        public RoomVisitedCondition(Room room, bool hasVisited = true) : base("roomvisited", room, hasVisited) { }

        public override bool TestCondition(GameState gs)
        {
            var room = Param1 ?? gs.Ego.CurrentRoom;
            
            if (Param2 == false)
            {
                return !room.HasVisited;
            }

            return room.HasVisited;
        }
	}
}
