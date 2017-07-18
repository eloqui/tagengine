using System;
using System.Collections.Generic;
using System.Text;

using TagEngine.Entities;

namespace TagEngine.Data
{
    [Serializable]
	public class GameState
	{

		#region Fields
        
		#endregion

		#region Properties

		/// <summary>
		/// Collection of all the items in the game
		/// </summary>
		public Entities<Item> Items { get; protected set; }

        /// <summary>
        /// Collection of all the rooms in the game
        /// </summary>
        public Entities<Room> Rooms { get; protected set; }

        /// <summary>
        /// Collection of all the NPCs in the game
        /// </summary>
        public Entities<Npc> Npcs { get; protected set; }

        /// <summary>
        /// Collection of variables
        /// </summary>
        public Variables Variables { get; protected set; }

        /// <summary>
        /// Current player
        /// </summary>
        public Ego Ego { get; protected set; }

        /// <summary>
        /// Welcome message to the game
        /// </summary>
        public string WelcomeMessage { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSetupFinalised { get; protected set; }

        #endregion

        public GameState()
		{
            IsSetupFinalised = false;

            Items = new Entities<Item>();
            Rooms = new Entities<Room>();
            Npcs = new Entities<Npc>();
            Variables = new Variables();

            WelcomeMessage = "Welcome!";
			Ego = new Ego("You", "It's just you.");

			Variables.Set("testvar1", "value");
			Variables.Set("test2", 123.456);
        }
        
        public void FinaliseSetup()
        {
            IsSetupFinalised = true;
        }

        public void AddItem(Item item)
        {
            if (IsSetupFinalised) throw new InvalidOperationException("Cannot add items after game state setup is finalised");

            Items.Add(item.Name, item);
        }

        public void AddRoom(Room room)
        {
            if (IsSetupFinalised) throw new InvalidOperationException("Cannot add rooms after game state setup is finalised");

            Rooms.Add(room.Name, room);
        }

        public void AddNpc(Npc npc)
        {
            if (IsSetupFinalised) throw new InvalidOperationException("Cannot add NPCs after game state setup is finalised");

            Npcs.Add(npc.Name, npc);
        }

        public void SetEgo(Ego ego)
        {
            if (IsSetupFinalised) throw new InvalidOperationException("Cannot set Ego after game state setup is finalised");

            Ego = ego;
        }

        public void SetCurrentLocation(Room room)
        {
            Ego.MoveTo(room);
        }

        public void SetWelcomeMessage(string welcomeMessage)
        {
            if (IsSetupFinalised) throw new InvalidOperationException("Cannot set welcome message after game state setup is finalised");

            WelcomeMessage = welcomeMessage;
        }
	}
}
