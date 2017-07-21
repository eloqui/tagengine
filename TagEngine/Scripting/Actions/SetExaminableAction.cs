using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Data;
using TagEngine.Entities;

namespace TagEngine.Scripting.Actions
{
    class SetExaminableAction : Action<InteractiveEntity, bool>
    {
        public SetExaminableAction(InteractiveEntity entity, bool isExaminable) : base(entity, isExaminable)
        {
        }

        public override ResponseMessage DoAction(GameState gs)
        {
            Param1.IsExaminable = Param2;

            return null;
        }
    }
}
