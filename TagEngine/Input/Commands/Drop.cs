using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Entities;
using TagEngine.Scripting;

namespace TagEngine.Input.Commands
{
    class Drop : Command
    {
        public Drop() : base("put", new List<string> { "drop", "place" }) { }

        /// <summary>
        /// A trigger for the drop command
        /// </summary>
        public class Trigger : Trigger<Item>
        {
            public Trigger(Item item) : base("drop", item) { }
        }

        protected override Response ProcessInternal(Engine engine, Tokeniser tokens)
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
                            ego.Inventory.RemoveItem(item);
                            ego.CurrentRoom.AddItem(item);

                            var response = engine.RunOccurrences(new Drop.Trigger(item));

                            if (!response.HasMessage)
                            {
                                response.AddMessage("You carefully drop the " + item.Title + ".");
                            }

                            return response;
                        }
                    }
                }
            }

            return new Response("Put down what?");
        }
    }
}
