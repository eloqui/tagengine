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
			
		}
	}
}
