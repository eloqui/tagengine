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
using TagEngine.Entities;

namespace TagEngine.Scripting
{
	/// <summary>
	/// An action with 1 parameter performed on game state
	/// </summary>
	public abstract class Action<TData1> : IAction
	{
        /// <summary>
        /// First parameter
        /// </summary>
        public TData1 Param1 { get; protected set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="param1"></param>
        protected Action(TData1 param1)
        {
            Param1 = param1;
        }

        /// <summary>
        /// Perform the tasks for this action
        /// </summary>
        /// <param name="gs"></param>
        /// <returns></returns>
        public abstract Response DoAction(GameState gs);
    }

    /// <summary>
    /// An action with 2 parameters performed on game state
    /// </summary>
    /// <typeparam name="TData1"></typeparam>
    /// <typeparam name="TData2"></typeparam>
    public abstract class Action<TData1, TData2> : Action<TData1>
    {
        /// <summary>
        /// Second parameter
        /// </summary>
        public TData2 Param2 { get; protected set; }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        protected Action(TData1 param1, TData2 param2) : base(param1)
        {
            Param2 = param2;
        }
    }

    /// <summary>
    /// An action with 3 parameters performed on game state
    /// </summary>
    /// <typeparam name="TData1"></typeparam>
    /// <typeparam name="TData2"></typeparam>
    /// <typeparam name="TData3"></typeparam>
    public abstract class Action<TData1, TData2, TData3> : Action<TData1, TData2>
    {
        /// <summary>
        /// Third parameter
        /// </summary>
        public TData3 Param3 { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        /// <param name="param3"></param>
        protected Action(TData1 param1, TData2 param2, TData3 param3) : base(param1, param2)
        {
            Param3 = param3;
        }
    }
}
