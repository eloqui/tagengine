using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Data;
using TagEngine.Entities;

namespace TagEngine.Scripting.Actions
{
    class RemoveItemFromRoomAction : Action<Room, Item>
    {
        public RemoveItemFromRoomAction(Room room, Item item) : base(room, item) { }

        public override Response DoAction(GameState gs)
        {
            if (Param1.HasItem(Param2)) Param1.RemoveItem(Param2);

            return null;
        }
    }
}
