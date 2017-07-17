using System;
using System.Collections.Generic;
using System.Text;
using TagEngine.Entities;

namespace TagEngine.Scripting
{
	/// <summary>
	/// An action performed on game state
	/// </summary>
	abstract public class Action
	{
		private object val;

		private Entity entity;

		private string okMessage = "";

		public object Val
		{
			get { return val; }
		}

		public Entity Entity
		{
			get { return entity; }
		}

		public void Act()
		{
			DoAction();
		}

		abstract protected void DoAction();
	}
}
