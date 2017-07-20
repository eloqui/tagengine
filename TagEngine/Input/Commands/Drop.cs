using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEngine.Input.Commands
{
    class Drop : Command
    {
        public Drop() : base("put", new List<string> { "drop", "place" }) { }

        public override Response Process(Engine engine, Tokeniser tokens)
        {
            var ego = engine.GameState.Ego;

            var possibles = tokens.Unrecognised;

            if (possibles.Count > 0)
            {
                foreach (var token in possibles)
                {
                    if (engine.GameState.IsValidItem(token.Word))
                    {
                        var item = engine.GameState.GetItem(token.Word);
                        if (ego.IsCarrying(item))
                        {
                            // remove the item from player inv and put it in the current room
                            ego.Inventory.Remove(item);
                            ego.CurrentRoom.AddItem(item);
                            
                            return new Response("You carefully drop the " + item.Title + ".");
                        }
                    }
                }
            }

            return new Response("Put down what?");
        }
    }
}
