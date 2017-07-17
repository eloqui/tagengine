using System;

namespace TagEngine.Entities
{
	/// <summary>
	/// Representation of an inventory item
	/// </summary>
	[Serializable]
	public class Item : MovableEntity
	{
		#region Fields

		/// <summary>
		/// The weight of this item
		/// </summary>
		private int weight;

		/// <summary>
		/// The message presented when the user picks this item up
		/// </summary>
		private string pickupMessage;

		/// <summary>
		/// Whether the user can pick this item up
		/// </summary>
		private bool canPickup;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the item's weight
		/// </summary>
		public int Weight
		{
			get { return weight; }
			set { weight = value; }
		}

		/// <summary>
		/// Gets or sets the item's pickup message
		/// </summary>
		public string PickupMessage
		{
			get { return pickupMessage; }
			set { pickupMessage = value; }
		}

		/// <summary>
		/// Gets or sets whether this item can be picked up
		/// </summary>
		public bool CanPickup
		{
			get { return canPickup; }
			set { canPickup = value; }
		}

		/// <summary>
		/// Gets or sets the extended description of this item
		/// </summary>
		public string Examination
		{
			get { return base.ExtendedDescription; }
			set { base.ExtendedDescription = value; }
		}

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
		public Item(string name, string title, string description, string extendedDescription, int weight)
			: base(name, title, description)
		{
			this.ExtendedDescription = extendedDescription;
			this.weight = weight;
			this.pickupMessage = "";
			this.canPickup = true;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The new item's name</param>
		/// <param name="title">The new item's title</param>
		/// <param name="description">The new item's description</param>
		/// <param name="extendedDescription">The new item's extended description</param>
		/// <param name="weight">The new item's weight</param>
		/// <param name="pickupMessage">The message given to the user when this item is picked up</param>
		public Item(string name, string title, string description, string extendedDescription, int weight, string pickupMessage)
			: base(name, title, description)
		{
			this.ExtendedDescription = extendedDescription;
			this.weight = weight;
			this.pickupMessage = pickupMessage;
			this.canPickup = true;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The new item's name</param>
		/// <param name="title">The new item's title</param>
		/// <param name="description">The new item's description</param>
		/// <param name="extendedDescription">The new item's extended description</param>
		/// <param name="weight">The new item's weight</param>
		/// <param name="pickupMessage">The message given to the user when this item is picked up</param>
		/// <param name="canPickup">Whether the new item can be picked up</param>
		public Item(string name, string title, string description, string extendedDescription, int weight, string pickupMessage, bool canPickup)
			: base(name, title, description)
		{
			this.ExtendedDescription = extendedDescription;
			this.weight = weight;
			this.pickupMessage = pickupMessage;
			this.canPickup = canPickup;
		}

		#endregion


	}
}
