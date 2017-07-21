using System;
using System.Collections.Generic;
using System.Text;

namespace TagEngine.Entities
{
	/// <summary>
	/// A variable
	/// </summary>
    [Serializable]
	public struct Variable
	{
        /// <summary>
        /// This variable's name
        /// </summary>
        public string Name;

        /// <summary>
        /// This variable's value
        /// </summary>
        public object Value;

		/// <summary>
		/// Constructor (sets Value to null)
		/// </summary>
		/// <param name="name">The variable's name</param>
		public Variable(string name)
		{
            Name = name;
			Value = null;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The variable name</param>
		/// <param name="value">The variable's initial value</param>
		public Variable(string name, object value)
		{
            Name = name;
			Value = value;
		}

        //public T GetValue<T>()
        //{
        //    return (T)Value;
        //}
	}

	/// <summary>
	/// A collection of variables
	/// </summary>
    [Serializable]
	public class Variables
	{
		/// <summary>
		/// Collection of variables
		/// </summary>
		readonly Dictionary<string, Variable> variables = new Dictionary<string, Variable>();

		/// <summary>
		/// Sets the value of a variable or creates it if it does not exist
		/// </summary>
		/// <param name="name">The name of the variable to change</param>
		/// <param name="value">The new value for the variable</param>
		public void Set(string name, object value)
		{
            Variable v;
            v.Name = name;
            v.Value = value;
			if (variables.ContainsKey(name))
			{
				variables[name] = v;
			}
			else
			{
				variables.Add(name, v);
			}
		}

		/// <summary>
		/// Get a variable from the collection
		/// </summary>
		/// <param name="name">The name of the variable to get</param>
		/// <returns>The value of the variable or null if it is uninitialised</returns>
		public object GetVariable(string name)
		{
            if (!variables.ContainsKey(name)) return null;

            return variables[name].Value;
		}

		/// <summary>
		/// Get a variable object from the collection
		/// </summary>
		/// <param name="name">The name of the variable to get</param>
		/// <returns>The variable (Value will be null if not initialised)</returns>
		public Variable GetVariableObject(string name)
		{
			if (variables.ContainsKey(name)) return variables[name];

			return new Variable(name);
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
				sb.AppendFormat("{0}: {1}\n", kvp.Key, kvp.Value.Value);
			}
			return sb.ToString();
		}
	}
}
