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
        List<string> Synonyms { get; }

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
