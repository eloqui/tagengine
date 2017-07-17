using System;
using System.Collections.Generic;
using System.Text;

using TagEngine.Data;

namespace TagEngine.Scripting
{
	/// <summary>
	/// A condition that must be met before an action can take place occur
	/// </summary>
	abstract public class Condition
	{
		private object data;

		private object val;

		public object Data
		{
			get { return data; }
		}

		public object Val
		{
			get { return val; }
		}

		/// <summary>
		/// Test this condition based on the current game state
		/// </summary>
		/// <param name="gs"></param>
		/// <returns></returns>
		public bool TestCondition(GameState gs, object data, object val)
		{
			this.data = data;
			this.val = val;
			return Check(gs);
		}

		/// <summary>
		/// Get whether this condition is met
		/// </summary>
		/// <returns>Whether the condition is met</returns>
		abstract protected bool Check(GameState gs);
	}
}
