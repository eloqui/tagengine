using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagEngine.Entities;

namespace TagEngine.Scripting
{
    /// <summary>
    /// A line or lines of dialogue that are accompanied by zero or more possible answers
    /// </summary>
    public class Cue
    {
        public string Text;

        public List<Answer> Answers { get; protected set; }

        public Cue(string text)
        {
            Answers = new List<Answer>();
            Text = text;
        }

        /// <summary>
        /// Create a Cue with a list of answers
        /// </summary>
        /// <param name="text"></param>
        /// <param name="answers"></param>
        public Cue(string text, params Answer[] answers)
        {
            Answers = new List<Answer>(answers);
            Text = text;
        }

        public void AddAnswer(Answer a)
        {
            Answers.Add(a);
        }
    }

    /// <summary>
    /// An answer to a cue, that can lead to another cue, and can have conditions to restrict its availability
    /// </summary>
    public class Answer
    {
        public class Trigger : Trigger<Answer>
        {
            // TODO: this maybe isn't the correct arg type?
            public Trigger(Answer answer) : base("answer", answer) { }
        }

        public string Text;

        public Cue Next;

        public List<ICondition> Conditions;

        public Answer(string text, Cue nextCue = null)
        {
            Conditions = new List<ICondition>();
            Text = text;
            Next = nextCue;
        }

        public void AddCondition(ICondition c)
        {
            Conditions.Add(c);
        }
    }

    /// <summary>
    /// A series of cues and answers
    /// </summary>
    public class Dialogue : Entity
    {
        /// <summary>
        /// Whether this dialogue has been spakened
        /// </summary>
        public bool HasSpake { get; set; } = false;

        /// <summary>
        /// The cues in this dialogue
        /// </summary>
        public List<Cue> Cues { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        public Dialogue(string name) : base("d_"+name)
        {
            Cues = new List<Cue>();
        }

        public void AddCue(Cue cue)
        {
            Cues.Add(cue);
        }
    }
}
