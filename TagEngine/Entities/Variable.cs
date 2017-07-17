using System;
using System.Collections.Generic;
using System.Text;

namespace TagEngine.Entities
{
	/// <summary>
	/// A variable
	/// </summary>
	public class Variable : Entity
	{
		/// <summary>
		/// The variable's value
		/// </summary>
		private object val;

		/// <summary>
		/// Gets or sets this variable's value
		/// </summary>
		public object Val
		{
			get { return val; }
			set { val = value; }
		}

		/// <summary>
		/// Constructor (sets Val to null)
		/// </summary>
		/// <param name="name">The variable's name</param>
		public Variable(string name)
			: base(name)
		{
			this.val = null;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The variable name</param>
		/// <param name="val">The variable's initial value</param>
		public Variable(string name, object val)
			: base(name)
		{
			this.val = val;
		}
	}

	/// <summary>
	/// A collection of variables
	/// </summary>
	public class Variables
	{
		/// <summary>
		/// Collection of variables
		/// </summary>
		private Dictionary<string, Variable> variables = new Dictionary<string,Variable>();

		/// <summary>
		/// Sets the value of a variable or creates it if it does not exist
		/// </summary>
		/// <param name="name">The name of the variable to change</param>
		/// <param name="value">The new value for the variable</param>
		public void Set(string name, object value)
		{
			if (variables.ContainsKey(name))
			{
				variables[name].Val = value;
			}
			else
			{
				variables.Add(name, new Variable(name, value));
			}
		}

		/// <summary>
		/// Get a variable from the collection
		/// </summary>
		/// <param name="name">The name of the variable to get</param>
		/// <returns>The value of the variable or null if it is uninitialised</returns>
		public object GetVariable(string name)
		{
			if (variables.ContainsKey(name)) return variables[name].Val;
			return null;
		}

		/// <summary>
		/// Get a variable object from the collection
		/// </summary>
		/// <param name="name">The name of the variable to get</param>
		/// <returns>The variable or null if it is uninitialised</returns>
		public Variable GetRawVariable(string name)
		{
			if (variables.ContainsKey(name)) return variables[name];
			return null;
		}

		/// <summary>
		/// Get a string representation of this variable collection
		/// </summary>
		/// <returns>A string containing the name and value of each variable in this collection</returns>
		public override string ToString()
		{
			if (variables.Count < 1) return "No variables.";

			StringBuilder sb = new StringBuilder();
			foreach (KeyValuePair<string, Variable> kvp in variables)
			{
				sb.AppendFormat("{0}: {1}\n", kvp.Key, kvp.Value.Val.ToString());
			}
			return sb.ToString();
		}
	}
}
