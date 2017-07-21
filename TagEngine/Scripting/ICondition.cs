using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Data;

namespace TagEngine.Scripting
{
    public interface ICondition
    {
        /// <summary>
        /// Test the condition
        /// </summary>
        /// <param name="gs"></param>
        /// <returns></returns>
        bool TestCondition(GameState gs);
    }
}
