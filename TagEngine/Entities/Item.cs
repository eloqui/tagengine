using System;
using System.Collections.Generic;

namespace TagEngine.Entities
{
	/// <summary>
	/// Representation of an inventory item
	/// </summary>
	[Serializable]
	public class Item : MovableEntity
	{
        #region Properties

        /// <summary>
        /// The weight of this item
        /// </summary>
        public int Weight { get; set; }

        /// <summary>
        /// The message presented when the user picks this item up
        /// </summary>
        public string PickupMessage { get; set; }

        /// <summary>
        /// Whether the user can pick this item up
        /// </summary>
        public bool CanPickup { get; set; }
        
		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The new item's name</param>
		/// <param name="title">The new item's title</param>
		/// <param name="description">The new item's description</param>
		/// <param name="extendedDescription">The new item's extended description</param>
		/// <param name="weight">The new item's weight</param>
		public Item(string name, string title, string description, string extendedDescription, int weight, string pickupMessage = "", bool canPickup = true)
			: base(name, title, description, extendedDescription)
		{
			Weight = weight;
			PickupMessage = pickupMessage;
			CanPickup = canPickup;
		}

		#endregion

        /// <summary>
        /// Get an item by casting from the name
        /// </summary>
        /// <param name="itemName"></param>
        public static explicit operator Item(string itemName)
        {
            return Engine.Instance.GameState.GetItem(itemName);
        }
	}

    /// <summary>
    /// A collection of items
    /// </summary>
    [Serializable]
    public class Items : HashSet<Item>
    {
        public Items(params Item[] items) : base(items) { }
        public Items(IEnumerable<Item> items) : base(items) { }
    }
}
