using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEngine.Input.Commands
{
    class Inventory : Command
    {
        public Inventory() : base("inv", new List<string> { "inventory", "carrying", "pack" }) { }

        public override Response Process(Engine engine, Tokeniser tokens)
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

            return new Response(sb.ToString());
        }
    }
}
