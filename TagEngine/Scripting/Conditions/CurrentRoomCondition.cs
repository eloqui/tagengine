using System;
using System.Collections.Generic;
using System.Text;
using TagEngine.Data;
using TagEngine.Entities;

namespace TagEngine.Scripting.Conditions
{
	class CurrentRoomCondition : Condition<Room>
	{
        public CurrentRoomCondition(Room room) : base("currentroom", room) { }

        public override bool TestCondition(GameState gs)
        {
            return gs.Ego.CurrentRoom == Param1;
        }
    }
}
