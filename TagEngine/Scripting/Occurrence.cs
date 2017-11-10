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
using TagEngine.Data;
using TagEngine.Entities;

namespace TagEngine.Scripting
{
    /// <summary>
    /// Activated within Commands, this causes Actions to run based on Conditions
    /// </summary>
    public class Occurrence : Entity
    {
        /// <summary>
        /// Actions run when the condition is true
        /// </summary>
        readonly List<IAction> actions;

        /// <summary>
        /// Actions run when the condition is false
        /// </summary>
        readonly List<IAction> failureActions;

        /// <summary>
        /// Conditions to test
        /// </summary>
        readonly List<ICondition> conditions;

        /// <summary>
        /// The trigger for this occurrence
        /// </summary>
        public ITrigger Trigger { get; protected set; }

        /// <summary>
        /// Indicates if the occurrence is active or not
        /// </summary>
        public bool IsActive { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public Occurrence(string name, bool isActive = true) : base(name)
        {
            actions = new List<IAction>();
            failureActions = new List<IAction>();
            conditions = new List<ICondition>();

            IsActive = isActive;
        }

        /// <summary>
        /// Check if the conditions for this trigger are met
        /// </summary>
        /// <param name="gs"></param>
        /// <returns></returns>
        public bool CheckConditions(GameState gs)
        {
            foreach (var condition in conditions)
            {
                if (condition.TestCondition(gs) == false) return false;
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gs"></param>
        public Response RunActions(GameState gs)
        {
            var r = new Response();
            if (CheckConditions(gs))
            {
                foreach (var action in actions)
                {
                    r.Merge(action.DoAction(gs));
                }
            }
            else
            {
                foreach (var action in failureActions)
                {
                    r.Merge(action.DoAction(gs));
                }
            }
            return r;
        }

        /// <summary>
        /// Add an action to this trigger to run when the condition is met
        /// </summary>
        /// <param name="action"></param>
        public void AddAction(IAction action)
        {
            actions.Add(action);
        }

        /// <summary>
        /// Add an action to this trigger to run when the condition is NOT met
        /// </summary>
        /// <param name="action"></param>
        public void AddFailureAction(IAction action)
        {
            failureActions.Add(action);
        }

        /// <summary>
        /// Add a condition to this trigger
        /// </summary>
        /// <param name="condition"></param>
        public void AddCondition(ICondition condition)
        {
            conditions.Add(condition);
        }

        //public void SetTrigger<T>(params object[] args) where T : ITrigger
        //{

        //}
        public void SetTrigger(ITrigger t)
        {
            Trigger = t;
        }
    }
}
