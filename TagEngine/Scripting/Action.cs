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
