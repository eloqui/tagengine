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

namespace TagEngine.Input
{
    public interface ICommand
    {
        /// <summary>
        /// The primary command word
        /// </summary>
        string Word { get; }

        /// <summary>
        /// Any synonyms that are considered synonyms of the command word
        /// </summary>
        List<string> Aliases { get; }

        /// <summary>
        /// Natural syntax commands can be placed anywhere in the input, non-natural must be first word
        /// </summary>
        bool IsNaturalSyntax { get; }

        /// <summary>
        /// Get a list of all the command words this command handles
        /// </summary>
        /// <returns></returns>
        string[] GetCommandWords();

        /// <summary>
        /// Process the command
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        Response Process(Engine engine, Tokeniser tokens);

        /// <summary>
        /// Get text to display when help is requested for this command
        /// </summary>
        /// <returns></returns>
        // TODO: this maybe should return a Response? or some method for prettification
        string GetHelpText();

    }
}
