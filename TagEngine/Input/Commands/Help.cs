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

namespace TagEngine.Input.Commands
{
    class Help : Command
    {
        public Help() : base("help", null, false) { }

        protected override Response ProcessInternal(Engine engine, Tokeniser tokens)
        {
            if (tokens.WordCount > 1)
            {
                // asking for help with some command
                try
                {
                    var c = CommandManager.GetCommand(tokens.GetTokenAtPosition(1).Word);
                    var text = c.GetHelpText();
                    if (!String.IsNullOrEmpty(text))
                    {
                        return new Response(text);
                    }
                }
                catch (CommandNotFoundException) { }
            }

            var sb = new StringBuilder();

            sb.Append("TODO: game specific help here" + Environment.NewLine);
            
            sb.Append("Available command words are:" + Environment.NewLine);
            
            sb.Append(" ");
            int ii = 0;
            foreach (string cmd in CommandManager.GetPrimaryCommandWords())
            {
                sb.Append(cmd + " ");
                if ((++ii % 6) == 0) sb.Append(Environment.NewLine + " ");
            }

            return new Response(sb.ToString());
        }
    }
}
