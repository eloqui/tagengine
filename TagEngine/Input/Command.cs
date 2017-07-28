using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TagEngine.Scripting;

namespace TagEngine.Input
{
    /*
    again            repeat the last command
    brief            use long and short room descriptions
    die              quit the game
    fullscore        show how your score was achieved
    inventory        list your possessions
    inventory wide   paragraphed inventory lists
    inventory tall   one item per line inventory lists
    long             always use long room descriptions
    normal           use long and short room descriptions
    noscript         turn transcripting off
    notify on/off    turn score notification on and off
    nouns            show current settings of "it", "him", "her"
    objects          list the objects you have held
    oops <word>      correct mistake in previous input
    places           list the places you have been
    pronouns         show current settings of "it", "him", "her"
    quit             quit the game
    quotes on/off    turn the display of quotations on and off
    restart          start the game again from the beginning
    restore          restore a saved game from a file
    save             save the game to a file
    score            show your current score
    script on/off    start and stop transcription to a file
    short            always use short room descriptions
    superbrief       always use short room descriptions
    undo             undo last command, if possible
    unscript         turn transcripting off
    verbose          always use long room descriptions
    verify           check that the story file is undamaged
    version          give version and release numbers
    wait             do nothing for a turn

Abbreviations:

    g for again
    i for inventory
    l for look
    n for north (and so on)
    o for oops
    q for quit
    x for examine
    z for wait (short for "zzz")
    */

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
        protected abstract Response ProcessInternal(Engine engine, Tokeniser tokens);

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
        /// Alias words for the command
        /// </summary>
        public List<string> Aliases { get; protected set; }

        /// <summary>
        /// Natural syntax commands can be placed anywhere in the input, non-natural must be first word
        /// </summary>
        public bool IsNaturalSyntax { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        protected Command() : this("", null) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="word"></param>
        /// <param name="aliases"></param>
        protected Command(string word, List<string> aliases, bool isNaturalSyntax = true)
        {
            Word = word;
            Aliases = aliases;
            IsNaturalSyntax = isNaturalSyntax;
        }

        /// <summary>
        /// Get all the command words (including primary and any synonyms)
        /// </summary>
        /// <returns></returns>
        public string[] GetCommandWords()
        {
            var words = Aliases ?? new List<string>();
            words.Insert(0, Word);
            return words.ToArray();
        }

        /// <summary>
        /// Process the command
        /// </summary>
        /// <param name="engine"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public Response Process(Engine engine, Tokeniser tokens)
        {
            return ProcessInternal(engine, tokens);
        }
    }

    /// <summary>
    /// Command manager.
    /// </summary>
    public static class CommandManager
    {
        /// <summary>
        /// All command words and the command they run
        /// </summary>
        static SortedDictionary<string, ICommand> commands;

        /// <summary>
        /// All primary command words
        /// </summary>
        static SortedSet<string> primaryCommands;

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
                //where type.IsSubclassOf(typeof(Command))
                where typeof(ICommand).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract
                select type;

            foreach (var type in subClasses)
            {
                var c = (ICommand)Activator.CreateInstance(type);
                if (primaryCommands.Contains(c.Word))
                {
                    throw new DuplicateCommandException(c.Word + " is a duplicate command");
                }
                primaryCommands.Add(c.Word);
                foreach (var word in c.GetCommandWords())
                {
                    if (commands.ContainsKey(word))
                    {
                        throw new DuplicateCommandException(c.Word + " is a duplicate command or alias");
                    }
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
            return commands.Keys.ToArray();
        }

        /// <summary>
        /// Get list of primary command words we know about
        /// </summary>
        /// <returns></returns>
        public static string[] GetPrimaryCommandWords()
        {
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

            if (!commands.ContainsKey(command))
            {
                throw new CommandNotFoundException(command);
            }

            return commands[command];
        }

        /// <summary>
        /// Get a Command based on type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ICommand GetCommand<T>() where T : Command
        {
            try
            {
                return commands.First(x => x.Value is T).Value;
            }
            catch (InvalidOperationException ioe)
            {
                throw new CommandNotFoundException(typeof(T).Name, ioe);
            }
        }

        /// <summary>
        /// Check if a word (in an optional position) is a command word
        /// </summary>
        /// <param name="command"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static bool IsCommand(string command, int position = 0)
        {
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
    class DuplicateCommandException : Exception
    {
        public DuplicateCommandException()
        {
        }

        public DuplicateCommandException(string message) : base(message)
        {
        }

        public DuplicateCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DuplicateCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    class CommandNotFoundException : Exception
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
