using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using TagEngine.Data;

namespace TagEngine.Entities
{
    /// <summary>
    /// A non-pickupable feature of a room that can be looked at/examined
    /// </summary>
    public class RoomFeature : InteractiveEntity
    {
        // add a collection of lookables that can be filled with non-items that can be looked at
        //       so that if there are room features of note, they can be examined
        public RoomFeature(string name, string title, string description, string extendedDescription = null)
            : base(name, title, description, extendedDescription)
        {

        }
    }

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
        private Dictionary<Direction, Room> exitKeys;

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
        public Dictionary<Direction, Room> ExitKeys
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

        /// <summary>
        /// Features of the room that can be looked at or examined
        /// </summary>
        public Dictionary<string, RoomFeature> Features { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The new room's name</param>
        /// <param name="title">The new room's title</param>
        /// <param name="description">The description of the new room</param>
        /// <param name="exits">An array describing available exits</param>
        public Room(string name, string title, string description, bool isAccessible = true)
            : base(name, title, description, isAccessible)
        {
            this.exitKeys = new Dictionary<Direction, Room>(6);
            this.items = new List<Item>();
            this.npcs = new List<Npc>();
            Features = new Dictionary<string, RoomFeature>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The new room's name</param>
        /// <param name="title">The new room's title</param>
        /// <param name="description">The description of the new room</param>
        /// <param name="exits">An array describing available exits</param>
        public Room(string name, string title, string description, Exits exits)
            : this(name, title, description)
        {
            SetExits(exits);
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
        public Room(string name, string title, string description, Exits exits, Item[] items, Npc[] npcs, RoomFeature[] features)
            : this(name, title, description, exits)
        {
            foreach (var item in items) AddItem(item);
            foreach (var npc in npcs) AddNpc(npc);
            foreach (var feature in features) AddFeature(feature);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Add a feature to the room
        /// </summary>
        /// <param name="feature"></param>
        public void AddFeature(RoomFeature feature)
        {
            Features.Add(feature.Name, feature);
        }
        // TODO: allow some method for updating features based on conditions etc.

        /// <summary>
        /// Check if the room has a feature
        /// </summary>
        /// <param name="featureName"></param>
        /// <returns></returns>
        public bool HasFeature(string featureName)
        {
            if (String.IsNullOrEmpty(featureName)) return false;

            return Features.ContainsKey(featureName);
        }

        /// <summary>
        /// Check if the room has a feature
        /// </summary>
        /// <param name="feature"></param>
        /// <returns></returns>
        public bool HasFeature(RoomFeature feature)
        {
            if (feature == null) return false;

            return Features.ContainsValue(feature);
        }

        /// <summary>
        /// Get a room feature by name
        /// </summary>
        /// <param name="featureName"></param>
        /// <returns></returns>
        public RoomFeature GetFeature(string featureName)
        {
            if (!HasFeature(featureName)) return null;

            return Features[featureName];
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
            if (item == null) return false;

            return this.items.Contains(item);
        }

        //public bool HasItem(string itemName)
        //{
        //    var matchingItems = from item in items where item.Name == itemName select item;
        //    return matchingItems.Count() > 0;
        //}

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
            if (npc == null) return false;

            return this.npcs.Contains(npc);
        }


        /// <summary>
        /// Add an exit to this room
        /// </summary>
        /// <param name="name">The exit direction to set</param>
        /// <param name="room">The room the new exit leads to</param>
        public void AddExit(Direction exit, Room room)
        {
            this.exitKeys.Add(exit, room);
        }

        /// <summary>
        /// Check if this room has a particular exit
        /// </summary>
        /// <param name="exit"></param>
        /// <returns></returns>
        public bool HasExit(Direction exit)
        {
            return exitKeys.ContainsKey(exit);
        }

        /// <summary>
        /// Get the next room in a particular direction
        /// </summary>
        /// <param name="direction">The exit direction</param>
        /// <returns>The next room in the given direction or null if no room that way</returns>
        public Room GetNextRoom(Direction direction)
        {
            if (!HasExit(direction)) return null;

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
            foreach (var exit in this.exitKeys) {
                if (i == r)
                {
                    return exit.Value;
                }
                i++;
            }
            return null;
        }

        /// <summary>
        /// Set all the exits
        /// </summary>
        /// <param name="exits"></param>
        public void SetExits(Exits exits)
        {
            foreach (var exit in exits)
            {
                AddExit(exit.Key, exit.Value);
            }
        }

        /// <summary>
        /// A list of exits
        /// </summary>
        public class Exits : List<KeyValuePair<Direction, Room>>
        {
            public void Add(Direction exit, Room room)
            {
                Add(new KeyValuePair<Direction, Room>(exit, room));
            }
        }

        #endregion

        #region Implementation

        /// <summary>
        /// Get a list of exits from this room
        /// </summary>
        /// <returns></returns>
        private string GetExitString()
		{
            if (exitKeys.Count == 0) return "";

            var returnString = new StringBuilder();
            returnString.Append("Exits:");
            foreach (var exit in exitKeys)
            {
                returnString.Append(" " + exit.Key.GetExitDirection());
            }
            return returnString.ToString();
		}

        /// <summary>
        /// Get a list of NPCs present in the room
        /// </summary>
        /// <returns></returns>
		private string GetNpcsString()
		{
			if (npcs.Count == 0) return "";

			var returnString = new StringBuilder();
			returnString.Append("Some people are here:");
			foreach (var npc in npcs)
			{
				returnString.Append(" " + npc.Title);
			}
			returnString.Append(Environment.NewLine);
			return returnString.ToString();
		}

        /// <summary>
        /// Get a list of items in this room
        /// </summary>
        /// <returns></returns>
		private string GetItemsString()
		{
			if (items.Count == 0) return "";

			StringBuilder returnString = new StringBuilder();
			returnString.Append("Around you, you see:");
			foreach (Item item in items)
			{
				returnString.Append(" " + item.Title);
			}
			returnString.Append(Environment.NewLine);
			return returnString.ToString();
		}

		#endregion

        /// <summary>
        /// Get a Room by casting from its name
        /// </summary>
        /// <param name="roomName"></param>
        public static explicit operator Room(string roomName)
        {
            return Engine.Instance.GameState.GetRoom(roomName);
        }
	}
}
