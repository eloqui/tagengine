using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEngine.Input.Commands
{
    class PickUp : Command
    {
        public PickUp() : base("get", new List<string> { "pick", "collect" }) { }

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
                        if (ego.CurrentRoom.HasItem(item))
                        {
                            if (!item.CanPickup)
                            {
                                return new Response(item.PickupMessage);
                            }

                            if (ego.Inventory.TotalWeight + item.Weight > ego.Inventory.MaxWeight)
                            {
                                return new Response("You haven't enough strength or space to carry that item!");
                            }

                            // add the item to inventory
                            ego.Inventory.AddItem(item);
                            ego.CurrentRoom.RemoveItem(item);

                            if (!String.IsNullOrEmpty(item.PickupMessage))
                            {
                                return new Response(item.PickupMessage);
                            }

                            return new Response("You pick up the " + item.Title + ", and place it in your pack.");
                        }
                    }
                }
            }

            return new Response("Pick up what?");
        }
    }
}
