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
