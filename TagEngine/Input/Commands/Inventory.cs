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
