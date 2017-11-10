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
    class Use : Command
    {
        public Use() : base("use", null) { }

        /// <summary>
        /// Trigger for a Use command
        /// </summary>
        public class Trigger : Trigger<Item>
        {
            public Trigger(Item item) : base("use", item) { }
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

                        if (ego.IsCarrying(item) || ego.CurrentRoom.HasItem(item))
                        {
                            // get result from any associated occurrences
                            var response = engine.RunOccurrences(new Use.Trigger(item));
                            if (!response.Empty) return response;
                        }
                    }
                }

                // try to combine items then
                if (possibles.Count > 1)
                {
                    try
                    {
                        // hand off to the combine command
                        return CommandManager.GetCommand<Combine>().Process(engine, tokens);
                    }
                    catch (CommandNotFoundException)
                    {
                        return new Response("You cannot use those things together.");
                    }
                }

                return new Response("You cannot use that.");
            }

            return new Response("Use what?");
        }
    }
}
