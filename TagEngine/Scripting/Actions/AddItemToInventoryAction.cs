using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Data;
using TagEngine.Entities;

namespace TagEngine.Scripting.Actions
{
    class AddItemToInventoryAction : Action<Item>
    {
        public AddItemToInventoryAction(Item item) : base(item)
        {
        }

        public override Response DoAction(GameState gs)
        {
            gs.Ego.Inventory.AddItem(Param1);

            return null;
        }
    }
}
