using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Entities;

namespace TagEngine.Input.Commands
{
    class Combine : Command
    {
        public Combine() : base("combine", new List<string> { "join" }) { }

        public override Response Process(Engine engine, Tokeniser tokens)
        {
            var ego = engine.GameState.Ego;
            var possibles = tokens.Unrecognised;

            if (possibles.Count > 1)
            {
                var itemsToCombine = new List<Item>();

                foreach (var token in possibles)
                {
                    if (engine.GameState.IsValidItem(token.Word))
                    {
                        var item = engine.GameState.GetItem(token.Word);

                        if (ego.IsCarrying(item) || ego.CurrentRoom.HasItem(item))
                        {
                            itemsToCombine.Add(item);
                        }
                    }
                }

                if (itemsToCombine.Count < 1)
                {
                    return new Response("Combine what?");
                }
                else if (itemsToCombine.Count < 2)
                {
                    return new Response("What should I combine that with?");
                }

                // TODO: get result from any associated actions..
                return new Response("You combine the items.");
            }

            return new Response("Combine what?");
        }
    }
}
