using System;
using System.Collections.Generic;
using System.Text;

using TagEngine.Entities;

namespace TagEngine.Data
{
	public class GameState
	{

		#region Fields

		/// <summary>
		/// Collection of all items in the game
		/// </summary>
		private Dictionary<string, Item> items = new Dictionary<string, Item>();

		/// <summary>
		/// Collection of all NPCs in the game
		/// </summary>
		private Dictionary<string, Npc> npcs = new Dictionary<string, Npc>();

		/// <summary>
		/// Collection of all rooms in the game
		/// </summary>
		private Dictionary<string, Room> rooms = new Dictionary<string, Room>();

		/// <summary>
		/// Game state global variables
		/// </summary>
		private Variables variables = new Variables();

		/// <summary>
		/// The player character
		/// </summary>
		private Ego ego;

		#endregion

		#region Properties

		/// <summary>
		/// Get the collection of all the items in the game
		/// </summary>
		public Dictionary<string, Item> Items
		{
			get { return items; }
		}

		/// <summary>
		/// Get the collection of all the rooms in the game
		/// </summary>
		public Dictionary<string, Room> Rooms
		{
			get { return rooms; }
		}

		/// <summary>
		/// Get the collection of all the NPCs in the game
		/// </summary>
		public Dictionary<string, Npc> Npcs
		{
			get { return npcs; }
		}

		/// <summary>
		/// Get the collection of variables
		/// </summary>
		public Variables Variables
		{
			get { return variables; }
		}

		/// <summary>
		/// Get the current player
		/// </summary>
		public Ego Ego
		{
			get { return ego; }
		}

		#endregion

		public GameState()
		{
			ego = new Ego("You", "It's just you.");

			Variables.Set("testvar1", "value");
			Variables.Set("test2", 123.456);
		}
	}
}
