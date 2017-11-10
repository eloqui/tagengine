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
