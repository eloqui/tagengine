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

namespace TagEngine.Scripting
{
    /// <summary>
    /// A trigger
    /// </summary>
    /// <typeparam name="TData1"></typeparam>
    abstract public class Trigger<TData1> : ITrigger
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public TData1 Subject { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        protected Trigger(string name, TData1 subject)
        {
            Name = name;
            Subject = subject;
        }

        /// <summary>
        /// Check if the subject of this trigger equals the provided one
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        protected virtual bool SubjectEquals(TData1 subject)
        {
            return Subject.Equals(subject);
        }

        /// <summary>
        /// Check if the trigger matches
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool Matches(ITrigger t)
        {
            if (t.GetType() == GetType())
            {
                var ct = (Trigger<TData1>)t;
                return ct.SubjectEquals(Subject);
            }
            return false;
        }
    }

    /// <summary>
    /// A trigger with 2 parameters
    /// </summary>
    /// <typeparam name="TData1"></typeparam>
    /// <typeparam name="TData2"></typeparam>
    abstract public class Trigger<TData1, TData2> : Trigger<TData1>
    {
        /// <summary>
        /// Second subject object
        /// </summary>
        public TData2 Subject2 { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="subject2"></param>
        protected Trigger(string name, TData1 subject, TData2 subject2) : base(name, subject)
        {
            Subject2 = subject2;
        }

        /// <summary>
        /// Check if the subjects are equal
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="subject2"></param>
        /// <returns></returns>
        protected virtual bool SubjectEquals(TData1 subject, TData2 subject2)
        {
            return Subject.Equals(subject) && Subject2.Equals(subject2);
        }

        /// <summary>
        /// Check if the trigger matches
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override bool Matches(ITrigger t)
        {
            if (t.GetType() == GetType())
            {
                var ct = (Trigger<TData1, TData2>)t;
                return ct.SubjectEquals(Subject, Subject2);
            }
            return false;
        }
    }
}
