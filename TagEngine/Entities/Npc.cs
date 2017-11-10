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
using System.Text;

namespace TagEngine.Entities
{
	/// <summary>
	/// Representation of a non-player character
	/// </summary>
	[Serializable]
	public class Npc : MovableEntity, IHasInventory
	{
        // TODO: add AI behaviours, e.g. Wander, Hostile etc.

		#region Fields
        
		/// <summary>
		/// This NPC's dialogue
		/// TODO: rewrite dialogue
		/// </summary>
		List<string> dialogue;

		/// <summary>
		/// Indicates current index of dialogue
		/// </summary>
		//[NonSerialized]
		//int dialogueCounter = 0;

		/// <summary>
		/// Whether this NPC should move randomly around the map
		/// </summary>
		//bool moveRandomly = false;

        #endregion

        #region Properties

        /// <summary>
        /// The list of items in this NPC's inventory
        /// </summary>
        public Inventory Inventory { get; protected set; }

		/// <summary>
		/// Gets or sets the default dialogue
		/// TODO: rewrite dialogue
		/// </summary>
		public string DefaultDialogue
		{
			get { return dialogue[dialogue.Count - 1] as string; }
			set { dialogue[dialogue.Count - 1] = value; }
		}

		/// <summary>
		/// Gets or sets whether this NPC should move randomly around the map
		/// </summary>
		//public bool MoveRandomly
		//{
		//	get { return moveRandomly; }
		//	set { moveRandomly = value; }
		//}

		#endregion
	
		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The NPC's new name</param>
		/// <param name="title">The NPC's new prettyfied name</param>
		/// <param name="description">The NPC's description</param>
		public Npc(string name, string title, string description)
			: base(name, title, description)
		{
			Inventory = new Inventory(20); // TODO: allow setting inventory weight
            dialogue = new List<string>
            {
                "Hello there!"
            };
        }

        #endregion

        /// <summary>
        /// Check if this NPC is carrying an item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool IsCarrying(Item item)
        {
            return Inventory.HasItem(item);
        }

        /// <summary>
        /// Check if this NPC is carrying an item
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public bool IsCarrying(string itemName)
        {
            return Inventory.HasItem(itemName);
        }

        /// <summary>
        /// Get an NPC by casting from the name
        /// </summary>
        /// <param name="npcName"></param>
        public static explicit operator Npc(string npcName)
        {
            return Engine.Instance.GameState.GetNpc(npcName);
        }
    }
}
