using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Data;
using TagEngine.Entities;

namespace TagEngine.Scripting.Actions
{
    class GiveItemToNpcAction : Action<Npc, Item>
    {
        public GiveItemToNpcAction(Npc npc, Item item) : base(npc, item) { }

        public override Response DoAction(GameState gs)
        {
            gs.Ego.Inventory.RemoveItem(Param2);
            Param1.Inventory.AddItem(Param2);

            return null;
        }
    }
}
