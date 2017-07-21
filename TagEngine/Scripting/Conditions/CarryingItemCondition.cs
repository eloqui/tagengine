using System;
using System.Collections.Generic;
using System.Text;
using TagEngine.Data;
using TagEngine.Entities;

namespace TagEngine.Scripting.Conditions
{
    class CarryingItemCondition : Condition<Item, bool>
    {
        public CarryingItemCondition(Item item, bool isCarrying = true) : base("carryingitem", item, isCarrying) { }

        public override bool TestCondition(GameState gs)
        {
            if (gs.Ego.Inventory.HasItem(Param1))
            {
                return Param2;
            }

            return !Param2;
        }
    }
}
