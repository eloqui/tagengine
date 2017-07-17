using System;
using System.Collections.Generic;
using System.Text;
using TagEngine.Entities;

namespace TagEngine.Scripting.Conditions
{
	class CarryingItemCondition : Condition
	{
		protected override bool Check(TagEngine.Data.GameState gs)
		{
			if (gs.Ego.Inventory.Contains((Item)Data))
			{
				return (bool)Val;
			}
			else
			{
				return !(bool)Val;
			}
		}
	}
}
