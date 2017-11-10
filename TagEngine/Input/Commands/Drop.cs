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
