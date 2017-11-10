/*
 * MIT License
 * 
 * Copyright (c) 2017 Polarity Studio
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
 * documentation files (the "Software"), to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions
 * of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
 * TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */

using System;
using System.Collections.Generic;
using System.Text;

using TagEngine.Data;

namespace TagEngine.Scripting
{
    /// <summary>
    /// A condition with 1 parameter that must be met before an action can take place occur
    /// </summary>
    abstract public class Condition<TParam1> : ICondition
    {
        /// <summary>
        /// Name of this condition
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// The first parameter
        /// </summary>
		public TParam1 Param1 { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="param1"></param>
        protected Condition(string name, TParam1 param1)
        {
            Name = name;
            Param1 = param1;
        }

        /// <summary>
        /// Test this condition based on the current game state
        /// </summary>
        /// <param name="gs"></param>
        /// <returns></returns>
        public abstract bool TestCondition(GameState gs);
    }

	/// <summary>
	/// A condition with 2 parameters that must be met before an action can take place occur
	/// </summary>
	abstract public class Condition<TParam1, TParam2> : Condition<TParam1>
	{
        /// <summary>
        /// The value to check
        /// </summary>
		public TParam2 Param2 { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        protected Condition(string name, TParam1 param1, TParam2 param2) : base(name, param1)
        {
            Param2 = param2;
        }
	}

    /// <summary>
	/// A condition with 3 parameters that must be met before an action can take place occur
	/// </summary>
	abstract public class Condition<TParam1, TParam2, TParam3> : Condition<TParam1, TParam2>
    {
        /// <summary>
        /// The 3rd parameter
        /// </summary>
        public TParam3 Param3 { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        protected Condition(string name, TParam1 param1, TParam2 param2, TParam3 param3) : base(name, param1, param2)
        {
            Param3 = param3;
        }
    }
}
