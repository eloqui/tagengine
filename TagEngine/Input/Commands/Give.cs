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
                else if (item == null)
                {
                    return new Response("Give " + npc.Name + " what?");
                }
                else if (npc == null)
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
