using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEngine.Scripting
{
    public interface ITrigger
    {
        string Name { get; }

        bool Matches(ITrigger t);
    }
}
