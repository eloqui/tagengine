using System;
using System.Collections.Generic;
using System.Text;
using TagEngine.Data;
using TagEngine.Entities;

namespace TagEngine.Scripting.Actions
{
	class SetVariableAction : Action<string, object>
	{
        public SetVariableAction(string variableName, object value) : base(variableName, value)
        {
        }
        
        public override ResponseMessage DoAction(GameState gs)
        {
            gs.Variables.Set(Param1, Param2);

            return null;
        }
    }
}
