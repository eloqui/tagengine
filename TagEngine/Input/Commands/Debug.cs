﻿/*
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

namespace TagEngine.Input.Commands
{
    class Debug : Command
    {
        /// <summary>
        /// List of subcommands the debug command handles
        /// </summary>
        static readonly string[] subCommands = {
                "variables",
                "setvariable",
                "getitem",
                "goroom",
            };

        public Debug() : base("debug", null, false) { }

        public override string GetHelpText()
        {
            return "Does things to the insides." + Environment.NewLine + GetDebugCommands();
        }

        protected override Response ProcessInternal(Engine engine, Tokeniser tokens)
        {
            Response r = new Response();

            if (tokens.WordCount > 1)
            {
                string command = tokens.GetTokenAtPosition(1).Word;

                switch (command)
                {
                    // display list of all variables and their current value
                    case "variables":
                        r.AddMessage(engine.GameState.Variables.ToString());
                        return r;
                        
                    // set a variable to a value
                    case "setvariable":
                        if (tokens.WordCount < 4)
                        {
                            r.AddMessage("debug setvariable [variable] [value]", ResponseMessageType.Warning);
                        }
                        else
                        {
                            string variableName = tokens.GetTokenAtPosition(2).Word;
                            string value = tokens.GetTokenAtPosition(3).Word; // TODO: cast to type??
                            engine.GameState.Variables.Set(variableName, value);
                            return r;
                        }
                        break;

                    case "getitem": // get an intem and put it in inventory (will remain in its current room too)
                        if (tokens.WordCount < 3)
                        {
                            r.AddMessage("debug getitem [item]", ResponseMessageType.Warning);
                        }
                        else
                        {
                            var itemName = tokens.GetTokenAtPosition(2).Word;
                            if (engine.GameState.IsValidItem(itemName))
                            {
                                var item = engine.GameState.GetItem(itemName);
                                if (!engine.GameState.Ego.Inventory.Contains(item))
                                {
                                    engine.GameState.Ego.Inventory.AddItem(item);
                                    r.AddMessage("Added the " + item.Name + " to player inventory");
                                    return r;
                                }

                                r.AddMessage("Already have that in inventory", ResponseMessageType.Warning);
                            }
                            else
                            {
                                r.AddMessage("Not valid item >" + itemName + "<", ResponseMessageType.Error);
                            }
                        }
                        break;

                    case "goroom": // move ego to a particular room
                        if (tokens.WordCount < 3)
                        {
                            r.AddMessage("debug goroom [room]", ResponseMessageType.Warning);
                        }
                        else
                        {
                            var roomName = tokens.GetTokenAtPosition(2).Word;
                            if (engine.GameState.IsValidRoom(roomName))
                            {
                                var room = engine.GameState.GetRoom(roomName);
                                engine.GameState.Ego.MoveTo(room);
                                r.AddMessage("Moved player to room " + room.Name);
                                return r;
                            }

                            r.AddMessage("Not valid room >" + roomName + "<", ResponseMessageType.Error);
                        }
                        break;

                    // an unknown debug command
                    default:
                        if (subCommands.Contains(command))
                        {
                            r.AddMessage("Debug command >" + command + "< not yet implemented.", ResponseMessageType.Warning);
                        }
                        else
                        {
                            r.AddMessage("Unknown debug command: >" + command + "<", ResponseMessageType.Error);
                        }
                        break;
                }
            }
            
            // show list of available debug subcommands
            r.AddMessage(GetDebugCommands());
            return r;
        }

        string GetDebugCommands()
        {
            var sb = new StringBuilder();
            sb.Append("Debug commands:" + Environment.NewLine);
            foreach (var c in subCommands)
            {
                sb.Append(" " + c + Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}
