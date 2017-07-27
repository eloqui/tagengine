using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Data;
using TagEngine.Entities;

namespace TagEngine.Scripting.Actions
{
    class SetAccessibilityAction : Action<InteractiveEntity, bool>
    {
        public SetAccessibilityAction(InteractiveEntity entity, bool isAccessible) : base(entity, isAccessible)
        {
        }

        public override Response DoAction(GameState gs)
        {
            Param1.IsAccessible = Param2;

            return null;
        }
    }
}
