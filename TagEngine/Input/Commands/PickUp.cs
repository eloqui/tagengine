/*
 * MIT License
 * 
 * Copyright (c) 2017 Polarity Studio
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
 * documentation files (the "Software"), to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions
 * of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
 * TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */

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
