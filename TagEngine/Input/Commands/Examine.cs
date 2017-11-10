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
    class Examine : Command
    {
        public Examine() : base("examine", null, false) { }

        /// <summary>
        /// Trigger for an examine command
        /// </summary>
        public class Trigger : Trigger<InteractiveEntity>
        {
            public Trigger(InteractiveEntity entity) : base("examine", entity) { }
        }

        protected override Response ProcessInternal(Engine engine, Tokeniser tokens)
        {
            var ego = engine.GameState.Ego;

            // get list of potential things to look at from the tokens
            var possibles = tokens.Unrecognised;
            if (possibles.Count < 1)
            {
                // nothing provided to examine
                return new Response("Examine what?");
            }

            var response = new Response();

            foreach (var token in possibles)
            {
                if (engine.GameState.IsValidItem(token.Word))
                {
                    var item = engine.GameState.GetItem(token.Word);

                    if (item.IsExaminable && (ego.CurrentRoom.HasItem(item) || ego.IsCarrying(item)))
                    {
                        // an item in the current room or inventory
                        response.AddMessage(item.Examine(true));
                        response.Merge(engine.RunOccurrences(new Examine.Trigger(item)));
                        return response;
                    }
                }

                if (engine.GameState.IsValidNpc(token.Word))
                {
                    var npc = engine.GameState.GetNpc(token.Word);

                    if (npc.IsExaminable && ego.CurrentRoom.HasNpc(npc))
                    {
                        // an npc in the current room
                        response.AddMessage(npc.Examine(true));
                        response.Merge(engine.RunOccurrences(new Examine.Trigger(npc)));
                        return response;
                    }
                }

                if (ego.CurrentRoom.HasFeature(token.Word))
                {
                    // a feature of the current room
                    var feature = ego.CurrentRoom.GetFeature(token.Word);

                    if (feature.IsExaminable)
                    {
                        response.AddMessage(feature.Examine(true));
                        response.Merge(engine.RunOccurrences(new Examine.Trigger(feature)));
                        return response;
                    }
                }
            }

            // item/npc not in inventory or current room
            return new Response("I can't examine that.");
        }
    }
}
