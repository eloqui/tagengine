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
