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
using TagEngine.Scripting;

namespace TagEngine.Input.Commands
{
    class Inventory : Command
    {
        public Inventory() : base("inv", new List<string> { "inventory", "carrying", "pack" }) { }

        /// <summary>
        /// Trigger for an inventory view command
        /// </summary>
        public class Trigger : Trigger<object>
        {
            public Trigger(object subject = null) : base("inv", subject) { }

            protected override bool SubjectEquals(object subject)
            {
                return true;
            }
        }

        protected override Response ProcessInternal(Engine engine, Tokeniser tokens)
        {
            var ego = engine.GameState.Ego;

            if (ego.Inventory.Count == 0)
            {
                return new Response("You are not carrying any items.");
            }

            var sb = new StringBuilder();
            sb.Append("You are carrying:" + Environment.NewLine + " ");
            int i = 0;
            foreach (var item in ego.Inventory)
            {
                // show at most six items per line
                sb.Append(item.Title + " ");
                if ((++i % 6) == 0) sb.Append(Environment.NewLine + " ");
            }

            // append total weight carried
            sb.Append(Environment.NewLine + "Weighing: " + ego.Inventory.TotalWeight);

            var response = new Response(sb.ToString());

            response.Merge(engine.RunOccurrences(new Inventory.Trigger()));

            return response;
        }
    }
}
