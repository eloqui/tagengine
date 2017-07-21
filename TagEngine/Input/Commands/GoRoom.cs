using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Data;

namespace TagEngine.Input.Commands
{
    class GoRoom : Command
    {
        public GoRoom()
            : base("go", new List<string> { "move", "walk" })
        {
        }

        public override Response Process(Engine engine, Tokeniser tokens)
        {
            var directionWord = tokens.Direction;
            if (directionWord == null || !WordStore.IsDirection(directionWord))
            {
                return new Response("Go where?");
            }

            var direction = WordStore.GetDirection(directionWord);
            var ego = engine.GameState.Ego;

            if (direction == Direction.Back)
            {
                if (ego.PreviousRoom != null)
                {
                    ego.MoveTo(ego.PreviousRoom);
                    return new Response(engine.Describe(ego.CurrentRoom));
                }

                return new Response("You've not been anywhere yet!");
            }

            // try to leave current room
            var newRoom = ego.CurrentRoom.GetNextRoom(direction);
            if (newRoom == null)
            {
                return new Response("You try to walk " + directionWord + ", but realise how bad a mistake that wasa when you walk straight into a solid wall. Your nose will hurt for days.");
            }

            if (!newRoom.IsAccessible)
            {
                return new Response("You try, but find that the door is locked.");
            }

            ego.MoveTo(newRoom);
            return new Response(engine.Describe(ego.CurrentRoom));
        }
    }
}
