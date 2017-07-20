using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEngine.Input.Commands
{
    class Use : Command
    {
        public Use() : base("use", null) { }

        public override Response Process(Engine engine, Tokeniser tokens)
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
                            // TODO: get result from any associated action objects...
                            return new Response("You use the " + token.Word);
                        }
                    }
                }

                // try to combine items then
                if (possibles.Count > 1)
                {
                    try
                    {
                        var combineCommand = CommandManager.GetCommand("combine");
                        return combineCommand.Process(engine, tokens);
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
