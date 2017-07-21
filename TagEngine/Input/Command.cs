using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace TagEngine.Input
{
    /// <summary>
    /// Base class for commands
    /// </summary>
    public abstract class Command : ICommand
    {
        /// <summary>
        /// Process the command
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public abstract Response Process(Engine engine, Tokeniser tokens);

        /// <summary>
        /// Get text to display when help is requested for this command
        /// </summary>
        /// <returns></returns>
        // TODO: this maybe should return a Response? or some method for prettification
        public virtual string GetHelpText() { return null; }

        /// <summary>
        /// The command primary word
        /// </summary>
        public string Word { get; protected set; }

        /// <summary>
        /// Synonym words for the command
        /// </summary>
        public List<string> Synonyms { get; protected set; }

        /// <summary>
        /// Natural syntax commands can be placed anywhere in the input, non-natural must be first word
        /// </summary>
        public bool IsNaturalSyntax { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Command()
            : this("", null)
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="word"></param>
        /// <param name="synonyms"></param>
        protected Command(string word, List<string> synonyms, bool isNaturalSyntax = true)
        {
            Word = word;
            Synonyms = synonyms;
            IsNaturalSyntax = isNaturalSyntax;
        }

        /// <summary>
        /// Get all the command words (including primary and any synonyms)
        /// </summary>
        /// <returns></returns>
        public string[] GetCommandWords()
        {
            var words = Synonyms ?? new List<string>();
            words.Insert(0, Word);
            return words.ToArray();
        }
    }

    public static class CommandManager
    {
        /// <summary>
        /// All command words and the command they run
        /// </summary>
        private static SortedDictionary<string, ICommand> commands = null;

        /// <summary>
        /// All primary command words
        /// </summary>
        private static SortedSet<string> primaryCommands = null;

        /// <summary>
        /// Initialise the collection of commands
        /// </summary>
        public static void Initialise()
        {
            commands = new SortedDictionary<string, ICommand>(StringComparer.CurrentCultureIgnoreCase);
            primaryCommands = new SortedSet<string>(StringComparer.CurrentCultureIgnoreCase);

            // detect all children of the Command class, and add them to the dictionary
            var subClasses =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                //where type.IsSubclassOf(typeof(ICommand))
                where typeof(ICommand).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract
                select type;

            foreach (var type in subClasses)
            {
                var c = (ICommand)Activator.CreateInstance(type);
                primaryCommands.Add(c.Word);
                foreach (var word in c.GetCommandWords())
                {
                    commands.Add(word, c);
                }
            }
        }

        /// <summary>
        /// Get a list of command words we know about
        /// </summary>
        /// <returns></returns>
        public static string[] GetCommandWords()
        {
            // lazy init
            if (commands == null) Initialise();

            return commands.Keys.ToArray();
        }

        /// <summary>
        /// Get list of primary command words we know about
        /// </summary>
        /// <returns></returns>
        public static string[] GetPrimaryCommandWords()
        {
            if (commands == null) Initialise();

            return primaryCommands.ToArray();
        }

        /// <summary>
        /// Get a Command based on a word
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static ICommand GetCommand(string command)
        {
            if (String.IsNullOrEmpty(command))
            {
                throw new CommandNotFoundException("No command provided");
            }

            // lazy initialise
            if (commands == null) Initialise();

            if (!commands.ContainsKey(command))
            {
                throw new CommandNotFoundException(command);
            }

            return commands[command];
        }

        /// <summary>
        /// Check if a word (in an optional position) is a command word
        /// </summary>
        /// <param name="command"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static bool IsCommand(string command, int position = 0)
        {
            if (commands == null) Initialise();

            if (commands.ContainsKey(command))
            {
                // if the command is not in position 0, it must not be a natural command
                if (position != 0 && !commands[command].IsNaturalSyntax) return false;

                return true;
            }

            return false;
        }
    }

    [Serializable]
    internal class CommandNotFoundException : Exception
    {
        public CommandNotFoundException()
        {
        }

        public CommandNotFoundException(string message) : base(message)
        {
        }

        public CommandNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CommandNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
