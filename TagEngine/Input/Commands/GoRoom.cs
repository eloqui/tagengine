using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
            //var d = tokens.Direction;
            //if (d == null || !IsValidDirection(d))
            //{
            //    return new Response("Go where?");
            //}

            //if (d == "back")
            //{
            //    if (Engine.Instance.PreviousRoom != null)
            //    {
            //        return Engine.Instance.Ego.MoveTo(Engine.Instance.PreviousRoom);
            //    } else
            //    {
            //        return new Response("You've not been anywhere yet!");
            //    }
            //}

            //// try to leave current room
            //var newRoom = Engine.Instance.Ego.Room.NextRoom(d);
        }
    }
}
