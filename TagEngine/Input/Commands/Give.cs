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
    class Give : Command
    {
        public Give() : base("give", new List<string> { "pass" }) { }

        /// <summary>
        /// Trigger for a give command
        /// </summary>
        public class Trigger : Trigger<Npc, Item>
        {
            public Trigger(Npc npc, Item item) : base("give", npc, item) { }
        }

        protected override Response ProcessInternal(Engine engine, Tokeniser tokens)
        {
            var ego = engine.GameState.Ego;

            var possibles = tokens.Unrecognised;

            if (possibles.Count > 0)
            {
                Npc npc = null;
                Item item = null;

                foreach (var token in possibles)
                {
                    if (engine.GameState.IsValidItem(token.Word))
                    {
                        item = engine.GameState.GetItem(token.Word);
                    }
                    if (engine.GameState.IsValidNpc(token.Word))
                    {
                        npc = engine.GameState.GetNpc(token.Word);
                    }

                    // if we've found candidates for both, we can stop looking further
                    if (item != null && npc != null) break;
                }

                if (item == null && npc == null)
                {
                    return new Response("Give who what?");
                }

                if (item == null)
                {
                    return new Response("Give " + npc.Name + " what?");
                }

                if (npc == null)
                {
                    return new Response("Give the " + item.Name + " to whom?");
                }

                engine.GameState.Ego.Inventory.RemoveItem(item);
                npc.Inventory.AddItem(item);

                var response = engine.RunOccurrences(new Give.Trigger(npc, item));

                if (!response.HasMessage) response.AddMessage("You give the " + item.Name + " to " + npc.Name);

                return response;
            }

            return new Response("Give what to whom?");
        }
    }
}
