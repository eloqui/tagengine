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

    //public static class OccurrenceManager
    //{
    //    public static void CheckAction(string action, object receiver)
    //    {
    //        var gs = Engine.Instance.GameState;

    //        foreach (var occurrence in gs.Occurrences.Values)
    //        {
    //            if (occurrence.IsActive && (occurrence.Trigger == action && occurrence.AppliesTo(receiver)))
    //            {
    //                if (occurrence.CheckConditions(gs))
    //                {

    //                }
    //            }
    //        }
    //    }
    //}
}
