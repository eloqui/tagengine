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
	public class Ego : MovableEntity, IHasInventory
	{
        /// <summary>
        /// The player's inventory
        /// </summary>
        public Inventory Inventory { get; protected set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="title">Title of the player character</param>
		/// <param name="description">Description of the player character</param>
		public Ego(string title, string description)
			: base("ego", title, description)
		{
            Inventory = new Inventory(20); // TODO: allow changing the max weight carryable
		}

        /// <summary>
        /// Check if the character is carrying an item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool IsCarrying(Item item)
        {
            return Inventory.HasItem(item);
        }

        /// <summary>
        /// Check if the character is carrying an item
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public bool IsCarrying(string itemName)
        {
            return Inventory.HasItem(itemName);
        }
    }
}
