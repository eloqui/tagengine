using System;
using System.Collections.Generic;
using System.Text;

namespace TagEngine.Entities
{
	/// <summary>
	/// Representation of a non-player character
	/// </summary>
	[Serializable]
	public class Npc : MovableEntity
	{
		#region Fields

		/// <summary>
		/// The list of items in this NPC's inventory
		/// </summary>
		private Inventory inventory;

		/// <summary>
		/// This NPC's dialogue
		/// TODO: rewrite dialogue
		/// </summary>
		private List<string> dialogue;

		/// <summary>
		/// Indicates current index of dialogue
		/// </summary>
		[NonSerialized]
		private int dialogueCounter = 0;

		/// <summary>
		/// Whether this NPC should move randomly around the map
		/// </summary>
		private bool moveRandomly = false;

		#endregion

		#region Properties

		/// <summary>
		/// Gets this NPC's inventory collection
		/// </summary>
		public Inventory Inventory
		{
			get { return inventory; }
		}

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
		public bool MoveRandomly
		{
			get { return moveRandomly; }
			set { moveRandomly = value; }
		}

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
			inventory = new Inventory(20); // TODO: allow setting inventory weight
            dialogue = new List<string>
            {
                "Hello there!"
            };
        }

		#endregion

		#region Methods



		#endregion

	}
}
