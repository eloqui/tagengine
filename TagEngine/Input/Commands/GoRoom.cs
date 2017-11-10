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
