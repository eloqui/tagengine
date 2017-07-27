using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Entities;
using TagEngine.Scripting;

namespace TagEngine.Input.Commands
{
    class Examine : Command
    {
        public Examine() : base("examine", null, false) { }

        /// <summary>
        /// Trigger for an examine command
        /// </summary>
        public class Trigger : Trigger<InteractiveEntity>
        {
            public Trigger(InteractiveEntity entity) : base("examine", entity) { }
        }

        protected override Response ProcessInternal(Engine engine, Tokeniser tokens)
        {
            var ego = engine.GameState.Ego;

            // get list of potential things to look at from the tokens
            var possibles = tokens.Unrecognised;
            if (possibles.Count < 1)
            {
                // nothing provided to examine
                return new Response("Examine what?");
            }

            var response = new Response();

            foreach (var token in possibles)
            {
                if (engine.GameState.IsValidItem(token.Word))
                {
                    var item = engine.GameState.GetItem(token.Word);

                    if (item.IsExaminable && (ego.CurrentRoom.HasItem(item) || ego.IsCarrying(item)))
                    {
                        // an item in the current room or inventory
                        response.AddMessage(item.Examine(true));
                        response.Merge(engine.RunOccurrences(new Examine.Trigger(item)));
                        return response;
                    }
                }

                if (engine.GameState.IsValidNpc(token.Word))
                {
                    var npc = engine.GameState.GetNpc(token.Word);

                    if (npc.IsExaminable && ego.CurrentRoom.HasNpc(npc))
                    {
                        // an npc in the current room
                        response.AddMessage(npc.Examine(true));
                        response.Merge(engine.RunOccurrences(new Examine.Trigger(npc)));
                        return response;
                    }
                }

                if (ego.CurrentRoom.HasFeature(token.Word))
                {
                    // a feature of the current room
                    var feature = ego.CurrentRoom.GetFeature(token.Word);

                    if (feature.IsExaminable)
                    {
                        response.AddMessage(feature.Examine(true));
                        response.Merge(engine.RunOccurrences(new Examine.Trigger(feature)));
                        return response;
                    }
                }
            }

            // item/npc not in inventory or current room
            return new Response("I can't examine that.");
        }
    }
}
