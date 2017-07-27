using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Data;
using TagEngine.Entities;

namespace TagEngine.Scripting.Actions
{
    class AddItemToRoomAction : Action<Room, Item>
    {
        public AddItemToRoomAction(Room room, Item item) : base(room, item) { }

        public override Response DoAction(GameState gs)
        {
            if (!Param1.HasItem(Param2)) Param1.AddItem(Param2);

            return null;
        }
    }
}
