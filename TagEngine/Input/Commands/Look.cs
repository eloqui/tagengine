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
    class Look : Command
    {
        public Look() : base("look", null) { }

        /// <summary>
        /// Trigger for a look command
        /// </summary>
        public class Trigger : Trigger<InteractiveEntity>
        {
            public Trigger(InteractiveEntity entity) : base("look", entity) { }
        }

        protected override Response ProcessInternal(Engine engine, Tokeniser tokens)
        {
            var ego = engine.GameState.Ego;

            // get list of potential things to look at from the tokens
            var possibles = tokens.Unrecognised;

            var response = new Response();

            foreach (var token in possibles)
            {
                if (engine.GameState.IsValidItem(token.Word)) {
                    var item = engine.GameState.GetItem(token.Word);

                    if (ego.CurrentRoom.HasItem(item) || ego.IsCarrying(item))
                    {
                        // an item in the current room or inventory
                        response.AddMessage(item.Describe());
                        response.Merge(engine.RunOccurrences(new Look.Trigger(item)));
                        return response;
                    }
                }
                if (engine.GameState.IsValidNpc(token.Word))
                {
                    var npc = engine.GameState.GetNpc(token.Word);

                    if (ego.CurrentRoom.HasNpc(npc))
                    {
                        response.AddMessage(npc.Describe());
                        response.Merge(engine.RunOccurrences(new Look.Trigger(npc)));
                        return response;
                    }
                }

                if (ego.CurrentRoom.HasFeature(token.Word))
                {
                    var feature = ego.CurrentRoom.GetFeature(token.Word);
                    
                    response.AddMessage(feature.Describe());
                    response.Merge(engine.RunOccurrences(new Look.Trigger(feature)));
                    return response;
                }
            }

            if (possibles.Count > 0)
            {
                // wanted to look at semething that we don't know about
                return new Response("I can't see one of those.");
            }

            // if no arguments to "look", return description of the current room
            response.AddMessage(ego.CurrentRoom.Examine(true));
            response.Merge(engine.RunOccurrences(new Look.Trigger(ego.CurrentRoom)));
            return response;
        }
    }
}
