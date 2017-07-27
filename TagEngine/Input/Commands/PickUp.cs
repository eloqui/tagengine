using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Entities;
using TagEngine.Scripting;

namespace TagEngine.Input.Commands
{
    class PickUp : Command
    {
        public PickUp() : base("get", new List<string> { "pick", "collect" }) { }

        /// <summary>
        /// Trigger for a pickup command
        /// </summary>
        public class Trigger : Trigger<Item>
        {
            public Trigger(Item item): base("pickup", item) { }
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

                            var response = engine.RunOccurrences(new PickUp.Trigger(item));

                            if (!response.HasMessage)
                            {
                                if (!String.IsNullOrEmpty(item.PickupMessage))
                                {
                                    response.AddMessage(item.PickupMessage);
                                }
                                else
                                {

                                    response.AddMessage("You pick up the " + item.Title + ", and place it in your pack.");
                                }
                            }
                            return response;
                        }
                    }
                }
            }

            return new Response("Pick up what?");
        }
    }
}
