using System;
using System.Collections.Generic;
using System.Text;

namespace TagEngine.Entities
{
	public class Ego : MovableEntity
	{
		/// <summary>
		/// The player's inventory
		/// </summary>
		private Inventory inventory;

		/// <summary>
		/// Get the player's inventory
		/// </summary>
		public Inventory Inventory
		{
			get { return inventory; }
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="title">Title of the player character</param>
		/// <param name="description">Description of the player character</param>
		public Ego(string title, string description)
			: base("ego", title, description)
		{
            inventory = new Inventory();
		}

        /// <summary>
        /// Check if the character is carrying an item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool IsCarrying(Item item)
        {
            return inventory.Contains(item);
        }

        /// <summary>
        /// Check if the character is carrying an item
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public bool IsCarrying(string itemName)
        {
            var item = Engine.Instance.GameState.GetItem(itemName);
            if (item == null) return false;

            return inventory.Contains(item);
        }
    }
}
