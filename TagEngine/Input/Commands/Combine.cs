using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Entities;
using TagEngine.Scripting;

namespace TagEngine.Input.Commands
{
    class Combine : Command
    {
        public Combine() : base("combine", new List<string> { "join" }) { }

        /// <summary>
        /// A trigger for the combine command
        /// </summary>
        public class Trigger : Trigger<Items>
        {
            public Trigger(Items items) : base("combine", items) { }
            public Trigger(params Item[] items) : this(new Items(items)) { }

            protected override bool SubjectEquals(Items subject)
            {
                // we override this so we don't care about the order of the subject items
                return Subject.SetEquals(subject);
            }
        }

        protected override Response ProcessInternal(Engine engine, Tokeniser tokens)
        {
            var ego = engine.GameState.Ego;
            var possibles = tokens.Unrecognised;

            if (possibles.Count > 1)
            {
                var itemsToCombine = new Items();

                foreach (var token in possibles)
                {
                    if (engine.GameState.IsValidItem(token.Word))
                    {
                        var item = engine.GameState.GetItem(token.Word);

                        if (ego.IsCarrying(item) || ego.CurrentRoom.HasItem(item))
                        {
                            itemsToCombine.Add(item);
                        }
                    }
                }
                
                if (itemsToCombine.Count < 1)
                {
                    return new Response("Combine what?");
                }

                if (itemsToCombine.Count < 2)
                {
                    return new Response("What should I combine that with?");
                }

                // get result from any associated occurrences
                var response = engine.RunOccurrences(new Combine.Trigger(itemsToCombine));
                if (!response.Empty) return response;
            }

            return new Response("Combine what?");
        }
    }
}
