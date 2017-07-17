using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace TagEngine.Entities
{
	/// <summary>
	/// Represents a room which initially has no exits.
	/// A hashtable is used to hold exits which are string representing the keys of the rooms
	/// located North, South, East, and/or West.
	/// </summary>
	[Serializable]
	public class Room : InteractiveEntity
	{
		#region Fields

		/// <summary>
		/// Hash of exits from this room
		/// </summary>
		private Dictionary<string, Room> exitKeys;

		/// <summary>
		/// Number of visits to this room
		/// TODO: move into gamestate object
		/// </summary>
		[NonSerialized]
		private int numVisits = 0;

		/// <summary>
		/// Items in this room
		/// </summary>
		private List<Item> items;

		/// <summary>
		/// NPCs in this room
		/// </summary>
		private List<Npc> npcs;

		/// <summary>
		/// Whether this room is a magic transporter room
		/// </summary>
		private bool isTransporter = false;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the description of this room
		/// </summary>
		public override string Description
		{
			get
			{
				return "You are in " + base.Description + ".\n" +
					GetItemsString() + GetNpcsString() + GetExitString();
			}
			set
			{
				base.Description = value;
			}
		}

		/// <summary>
		/// Gets or sets the extended description of this room
		/// </summary>
		public override string ExtendedDescription
		{
			get
			{
				if (base.ExtendedDescription == "")
				{
					return "";
				}
				else
				{
					return "You are in " + base.Description + ".\n" +
						base.ExtendedDescription + "\n" +
						GetItemsString() + GetNpcsString() + GetExitString();
				}
			}
			set
			{
				base.ExtendedDescription = value;
			}
		}

		/// <summary>
		/// Gets the hash containing this room's available exits
		/// </summary>
		public Dictionary<string, Room> ExitKeys
		{
			get { return exitKeys; }
		}

		/// <summary>
		/// Gets or sets the number of times the player has visited this room
		/// </summary>
		public int NumVisits
		{
			get { return numVisits; }
			set { numVisits = value; }
		}

		/// <summary>
		/// Gets whether the player has previously visited this room
		/// </summary>
		public bool HasVisited
		{
			get { return numVisits > 0; }
		}

		/// <summary>
		/// Gets or sets whether this room is a transporter
		/// </summary>
		public bool IsTransporter
		{
			get { return isTransporter; }
			set { isTransporter = value; }
		}

		/// <summary>
		/// Gets the list of items in this room
		/// </summary>
		public List<Item> Items
		{
			get { return items; }
		}

		/// <summary>
		/// Gets the list of NPCs in this room
		/// </summary>
		public List<Npc> Npcs
		{
			get { return npcs; }
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The new room's name</param>
		/// <param name="title">The new room's title</param>
		/// <param name="description">The description of the new room</param>
		/// <param name="exits">An array describing available exits</param>
		public Room(string name, string title, string description)
			: base(name, title, description)
		{
			this.exitKeys = new Dictionary<string, Room>(4);
			this.items = new List<Item>();
			this.npcs = new List<Npc>();
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The new room's name</param>
		/// <param name="title">The new room's title</param>
		/// <param name="description">The description of the new room</param>
		/// <param name="exits">An array describing available exits</param>
		/// <param name="isAccessible">Whether the room is accessible</param>
		public Room(string name, string title, string description, bool isAccessible)
			: base(name, title, description, isAccessible)
		{
			this.items = new List<Item>();
			this.npcs = new List<Npc>();
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The new room's name</param>
		/// <param name="title">The new room's title</param>
		/// <param name="description">The description of the new room</param>
		/// <param name="exits">An array describing available exits</param>
		public Room(string name, string title, string description, string[] exits)
			: base(name, title, description)
		{
			//SetExits(exits);
			this.items = new List<Item>();
			this.npcs = new List<Npc>();
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The new room's name</param>
		/// <param name="title">The new room's title</param>
		/// <param name="description">The description of the new room</param>
		/// <param name="exits">An array describing available exits</param>
		/// <param name="items">An array of item keys to add</param>
		/// <param name="npcs">An array of NPC keys to add</param>
		public Room(string name, string title, string description, string[] exits, Item[] items, Npc[] npcs)
			: base(name, title, description)
		{
			//SetExits(exits);
			foreach (Item item in items) this.items.Add(item);
			foreach (Npc npc in npcs) this.npcs.Add(npc);
		}

		#endregion

		#region Methods

		/// <summary>
		/// Add an exit to this room
		/// </summary>
		/// <param name="name">Name of the exit (e.g. north, south, etc.)</param>
		/// <param name="room">The room the new exit leads to</param>
		public void AddExit(string name, Room room)
		{
			this.exitKeys.Add(name, room);
		}

		/// <summary>
		/// Add a collectable item to this room.
		/// </summary>
		/// <param name="item">The new item</param>
		public void AddItem(Item item)
		{
			this.items.Add(item);
		}

		/// <summary>
		/// Remove a collectable item from this room
		/// </summary>
		/// <param name="item">The item to remove</param>
		public void RemoveItem(Item item)
		{
			this.items.Remove(item);
		}

		/// <summary>
		/// Check if this room has a particular item
		/// </summary>
		/// <param name="item">The item</param>
		/// <returns>True if the room has the specified item</returns>
		public bool HasItem(Item item)
		{
			return this.items.Contains(item);
		}

		/// <summary>
		/// Add an NPC to this room
		/// </summary>
		/// <param name="npc">The new NPC to add</param>
		public void AddNpc(Npc npc)
		{
			this.npcs.Add(npc);
		}

		/// <summary>
		/// Remove an NPC from this room
		/// </summary>
		/// <param name="npc">The NPC to remove</param>
		public void RemoveNpc(Npc npc)
		{
			this.npcs.Remove(npc);
		}

		/// <summary>
		/// Check if this room has a particular NPC
		/// </summary>
		/// <param name="npc">The NPC</param>
		/// <returns>True if the room has the specified NPC</returns>
		public bool HasNpc(Npc npc)
		{
			return this.npcs.Contains(npc);
		}

		/// <summary>
		/// Get the next room in a particular direction
		/// </summary>
		/// <param name="direction">A valid direction name</param>
		/// <returns>The next room in the given direction</returns>
		public Room GetNextRoom(string direction)
		{
			return this.exitKeys[direction];
		}

		/// <summary>
		/// Get a random connected room
		/// </summary>
		/// <returns>The key of the random connected room</returns>
		public Room GetRandomExit()
		{
			int r = new Random().Next(0, this.exitKeys.Count);
			int i = 0;
			foreach (string key in this.exitKeys.Keys) {
				if (i == r)
				{
					return this.exitKeys[key];
				}
				i++;
			}
			return null;
		}

		#endregion

		#region Implementation

		private void SetExits(string[] exits)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		private string GetExitString()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		private string GetNpcsString()
		{
			if (npcs.Count == 0)
				return null;

			StringBuilder returnString = new StringBuilder();
			returnString.Append("Some people are here:");
			foreach (Npc npc in npcs)
			{
				returnString.Append(" " + npc.Title);
			}
			returnString.Append("\n");
			return returnString.ToString();
		}

		private string GetItemsString()
		{
			if (items.Count == 0)
				return "";

			StringBuilder returnString = new StringBuilder();
			returnString.Append("Around you, you see:");
			foreach (Item item in items)
			{
				returnString.Append(" " + item.Title);
			}
			returnString.Append("\n");
			return returnString.ToString();
		}

		#endregion
	}
}
