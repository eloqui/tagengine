using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEngine.Input.Commands
{
    class Look : Command
    {
        public Look() : base("look", null) { }

        public override Response Process(Engine engine, Tokeniser tokens)
        {
            var ego = engine.GameState.Ego;

            // get list of potential things to look at from the tokens
            var possibles = tokens.Unrecognised;
            
            foreach (var token in possibles)
            {
                if (engine.GameState.IsValidItem(token.Word)) {
                    var item = engine.GameState.GetItem(token.Word);

                    if (ego.CurrentRoom.HasItem(item) || ego.IsCarrying(item))
                    {
                        // an item in the current room or inventory
                        return new Response(engine.Describe(item));
                    }
                }
                if (engine.GameState.IsValidNpc(token.Word))
                {
                    var npc = engine.GameState.GetNpc(token.Word);

                    if (ego.CurrentRoom.HasNpc(npc))
                    {
                        return new Response(engine.Describe(npc));
                    }
                }

                if (ego.CurrentRoom.HasFeature(token.Word))
                {
                    var feature = ego.CurrentRoom.GetFeature(token.Word);

                    return new Response(engine.Describe(feature));
                }
            }

            if (possibles.Count > 0)
            {
                // wanted to look at semething that we don't know about
                return new Response("I can't see one of those.");
            }

            // if no arguments to "look", return description of the current room
            return new Response(engine.Examine(ego.CurrentRoom, true));
        }
    }
}
