using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Data;
using TagEngine.Entities;
using TagEngine.Scripting;

namespace TagEngine.Input.Commands
{
    class GoRoom : Command
    {
        public GoRoom() : base("go", new List<string> { "move", "walk" }) { }

        /// <summary>
        /// Trigger for a goroom command
        /// </summary>
        public class Trigger : Trigger<Room>
        {
            public Trigger(Room room) : base("goroom", room) { }
        }

        protected override Response ProcessInternal(Engine engine, Tokeniser tokens)
        {
            var directionWord = tokens.Direction;
            if (directionWord == null || !WordStore.IsDirection(directionWord))
            {
                return new Response("Go where?");
            }

            var direction = WordStore.GetDirection(directionWord);
            var ego = engine.GameState.Ego;

            var response = new Response();
            Room newRoom;

            if (direction == Direction.Back)
            {
                if (ego.PreviousRoom != null)
                {
                    newRoom = ego.PreviousRoom;
                }
                else
                {
                    return new Response("You've not been anywhere yet!");
                }
            }
            else
            {
                newRoom = ego.CurrentRoom.GetNextRoom(direction);
            }

            if (newRoom == null)
            {
                return new Response("You try to walk " + directionWord + ", but realise how bad a mistake that wasa when you walk straight into a solid wall. Your nose will hurt for days.");
            }

            if (!newRoom.IsAccessible)
            {
                response.AddMessage("You try, but find that the door is locked.");
            }
            else
            {
                ego.MoveTo(newRoom);
                response.AddMessage(newRoom.Describe());
            }
            response.Merge(engine.RunOccurrences(new GoRoom.Trigger(newRoom)));
            return response;
        }
    }
}
