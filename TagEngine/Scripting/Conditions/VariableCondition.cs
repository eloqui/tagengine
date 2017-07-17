using System;
using System.Collections.Generic;
using System.Text;

namespace TagEngine.Scripting.Conditions
{
	class VariableCondition : Condition
	{
		protected override bool Check(TagEngine.Data.GameState gs)
		{
			return gs.Variables.GetVariable((string)Data) == Val;
		}
	}
}
