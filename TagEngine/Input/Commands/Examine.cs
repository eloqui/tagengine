using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEngine.Input.Commands
{
    class Examine : Command
    {
        public Examine() : base("examine", null, false) { }

        public override Response Process(Engine engine, Tokeniser tokens)
        {
            var ego = engine.GameState.Ego;

            // get list of potential things to look at from the tokens
            var possibles = tokens.Unrecognised;
            if (possibles.Count < 1)
            {
                // nothing provided to examine
                return new Response("Examine what?");
            }

            foreach (var token in possibles)
            {
                if (engine.GameState.IsValidItem(token.Word))
                {
                    var item = engine.GameState.GetItem(token.Word);

                    if (item.IsExaminable && (ego.CurrentRoom.HasItem(item) || ego.IsCarrying(item)))
                    {
                        // an item in the current room or inventory
                        return new Response(engine.Examine(item, true));
                    }
                }

                if (engine.GameState.IsValidNpc(token.Word))
                {
                    var npc = engine.GameState.GetNpc(token.Word);

                    if (npc.IsExaminable && ego.CurrentRoom.HasNpc(npc))
                    {
                        // an npc in the current room
                        return new Response(engine.Examine(npc, true));
                    }
                }

                if (ego.CurrentRoom.HasFeature(token.Word))
                {
                    // a feature of the current room
                    var feature = ego.CurrentRoom.GetFeature(token.Word);

                    if (feature.IsExaminable) return new Response(engine.Examine(feature, true));
                }
            }

            // item/npc not in inventory or current room
            return new Response("I can't examine that.");
        }
    }
}
