using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Data;

namespace TagEngine.Scripting
{
    public interface IAction
    {
        ResponseMessage DoAction(GameState gs);
    }
}
