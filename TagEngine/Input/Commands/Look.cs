using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Entities;
using TagEngine.Scripting;

namespace TagEngine.Input.Commands
{
    class Look : Command
    {
        public Look() : base("look", null) { }

        /// <summary>
        /// Trigger for a look command
        /// </summary>
        public class Trigger : Trigger<InteractiveEntity>
        {
            public Trigger(InteractiveEntity entity) : base("look", entity) { }
        }

        protected override Response ProcessInternal(Engine engine, Tokeniser tokens)
        {
            var ego = engine.GameState.Ego;

            // get list of potential things to look at from the tokens
            var possibles = tokens.Unrecognised;

            var response = new Response();

            foreach (var token in possibles)
            {
                if (engine.GameState.IsValidItem(token.Word)) {
                    var item = engine.GameState.GetItem(token.Word);

                    if (ego.CurrentRoom.HasItem(item) || ego.IsCarrying(item))
                    {
                        // an item in the current room or inventory
                        response.AddMessage(item.Describe());
                        response.Merge(engine.RunOccurrences(new Look.Trigger(item)));
                        return response;
                    }
                }
                if (engine.GameState.IsValidNpc(token.Word))
                {
                    var npc = engine.GameState.GetNpc(token.Word);

                    if (ego.CurrentRoom.HasNpc(npc))
                    {
                        response.AddMessage(npc.Describe());
                        response.Merge(engine.RunOccurrences(new Look.Trigger(npc)));
                        return response;
                    }
                }

                if (ego.CurrentRoom.HasFeature(token.Word))
                {
                    var feature = ego.CurrentRoom.GetFeature(token.Word);
                    
                    response.AddMessage(feature.Describe());
                    response.Merge(engine.RunOccurrences(new Look.Trigger(feature)));
                    return response;
                }
            }

            if (possibles.Count > 0)
            {
                // wanted to look at semething that we don't know about
                return new Response("I can't see one of those.");
            }

            // if no arguments to "look", return description of the current room
            response.AddMessage(ego.CurrentRoom.Examine(true));
            response.Merge(engine.RunOccurrences(new Look.Trigger(ego.CurrentRoom)));
            return response;
        }
    }
}
