using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TagEngine.Input
{
    public abstract class Command
    {
        public abstract Response Process(Engine engine, Tokeniser tokens);

        public string Word { get; protected set; }
        public List<string> Synonyms { get; protected set; }

        public Command()
            : this("", null)
        {

        }

        protected Command(string word, List<string> synonyms)
        {
            Word = word;
            Synonyms = synonyms;
        }

        public string[] GetCommandWords()
        {
            var words = Synonyms ?? new List<string>();
            words.Insert(0, Word);
            return words.ToArray();
        }
    }

    public static class CommandManager
    {
        private static Dictionary<string, Command> commands = null;

        private static List<string> primaryCommands = null;

        /// <summary>
        /// Initialise the collection of commands
        /// </summary>
        public static void Initialise()
        {
            commands = new Dictionary<string, Command>();
            primaryCommands = new List<string>();

            // detect all children of the Command class, and add them to the dictionary
            var subClasses =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                where type.IsSubclassOf(typeof(Command))
                select type;

            foreach (var type in subClasses)
            {
                var c = (Command)Activator.CreateInstance(type);
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
        /// Get a Command based on a word
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static Command GetCommand(string command)
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
