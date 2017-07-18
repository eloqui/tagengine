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
        /// Exit types
        /// </summary>
        public enum Exit
        {
            North,
            East,
            South,
            West,
            Up,
            Down
        }

        /// <summary>
        /// Hash of exits from this room
        /// </summary>
        private Dictionary<Exit, Room> exitKeys;

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
        public Dictionary<Exit, Room> ExitKeys
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
            this.exitKeys = new Dictionary<Exit, Room>(6);
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
            this.exitKeys = new Dictionary<Exit, Room>(6);
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
            this.exitKeys = new Dictionary<Exit, Room>(6);
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
            this.exitKeys = new Dictionary<Exit, Room>(6);
            //SetExits(exits);
            foreach (Item item in items) this.items.Add(item);
            foreach (Npc npc in npcs) this.npcs.Add(npc);
        }

        #endregion

        #region Methods

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
        /// Add an exit to this room
        /// </summary>
        /// <param name="name">The exit to set</param>
        /// <param name="room">The room the new exit leads to</param>
        public void AddExit(Exit exit, Room room)
        {
            this.exitKeys.Add(exit, room);
        }

        /// <summary>
        /// Get the next room in a particular direction
        /// </summary>
        /// <param name="direction">A direction</param>
        /// <returns>The next room in the given direction or null if no room that way</returns>
        public Room GetNextRoom(Exit direction)
        {
            return this.exitKeys[direction] ?? null;
        }

        /// <summary>
        /// Get a random connected room
        /// </summary>
        /// <returns>The key of the random connected room</returns>
        public Room GetRandomExit()
        {
            int r = new Random().Next(0, this.exitKeys.Count);
            int i = 0;
            foreach (var exit in this.exitKeys) {
                if (i == r)
                {
                    return exit.Value;
                }
                i++;
            }
            return null;
        }

        public void SetExits(Exits exits)
        {
            foreach (var exit in exits)
            {
                AddExit(exit.Key, exit.Value);
            }
        }

        public class Exits : List<KeyValuePair<Exit, Room>>
        {
            public void Add(Exit exit, Room room)
            {
                Add(new KeyValuePair<Exit, Room>(exit, room));
            }
        }

        #endregion

        #region Implementation

        private string GetExitString()
		{
            if (exitKeys.Count == 0) return "";

            StringBuilder returnString = new StringBuilder();
            returnString.Append("Exits:");
            foreach (var exit in exitKeys)
            {
                returnString.Append(" " + exit.Key.GetExitDirection());
            }
            return returnString.ToString();
		}

		private string GetNpcsString()
		{
			if (npcs.Count == 0) return "";

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
			if (items.Count == 0) return "";

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

    public static class RoomExtensions
    {
        /// <summary>
        /// Get the direction string for a Room.Exit
        /// </summary>
        /// <param name="exit"></param>
        /// <returns>The direction string</returns>
        public static string GetExitDirection(this Room.Exit exit)
        {
            switch (exit)
            {
                case Room.Exit.North: return "north"; // TODO: probably load from the WordStore directions array by index?
                case Room.Exit.East: return "east";
                case Room.Exit.South: return "south";
                case Room.Exit.West: return "west";
                case Room.Exit.Up: return "up";
                case Room.Exit.Down: return "down";
            }

            return ""; // TODO: probably throw an exception here
        }
    }
}
