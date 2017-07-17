using System;
using System.Collections.Generic;
using System.Text;

using TagEngine.Entities;

namespace TagEngine.Scripting.Actions
{
	class ChangeVariableAction : Action
	{
		protected override void DoAction()
		{
			((Variable)this.Entity).Val = Val;
		}
	}
}
