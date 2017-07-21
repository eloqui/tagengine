using System;
using System.Collections.Generic;
using System.Text;
using TagEngine.Data;

namespace TagEngine.Scripting.Conditions
{
	class VariableCondition : Condition<string, object>
	{
        public VariableCondition(string variableName, object value) : base("variable", variableName, value) { }

        public override bool TestCondition(GameState gs)
		{
			return gs.Variables.GetVariable(Param1) == Param2;
		}
	}
}
