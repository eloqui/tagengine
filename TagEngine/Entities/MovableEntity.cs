using System;
using System.Collections.Generic;
using System.Text;

namespace TagEngine.Entities
{
	/// <summary>
	/// A movable entity
	/// </summary>
	[Serializable]
	public class MovableEntity : InteractiveEntity, IMovable
	{
		#region Fields
        
		#endregion

		#region Properties

		/// <summary>
		/// The room this entity is currently in
		/// </summary>
		public Room CurrentRoom { get; protected set; }

        /// <summary>
        /// The room this entity was previously in
        /// </summary>
        public Room PreviousRoom { get; protected set; }

		#endregion

		#region Constructors
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the entity</param>
        /// <param name="title">Title of the entity</param>
        /// <param name="description">Description of the entity</param>
        /// <param name="extendedDescription">Long description of the entity</param>
        public MovableEntity(string name, string title, string description, string extendedDescription = null)
            : base(name, title, description, extendedDescription)
        {
            CurrentRoom = null;
            PreviousRoom = null;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the entity</param>
        /// <param name="title">Title of the entity</param>
        /// <param name="description">Description of the entity</param>
        /// <param name="room">The room to start in</param>
        public MovableEntity(string name, string title, string description, Room room)
			: base(name, title, description)
		{
			CurrentRoom = room;
            PreviousRoom = null;
		}

		#endregion


		#region Methods

		/// <summary>
		/// Move this entity to another room
		/// </summary>
		/// <param name="room">The room to move to</param>
		public void MoveTo(Room room)
		{
            PreviousRoom = CurrentRoom;
			CurrentRoom = room;
		}

		/// <summary>
		/// Move this entity to another room
		/// </summary>
		/// <param name="roomName">The name of the room to move to</param>
		public void MoveTo(string roomName)
		{
			MoveTo(Engine.Instance.GameState.Rooms[roomName]);
		}

		/// <summary>
		/// Check if this entity is in a particular room
		/// </summary>
		/// <param name="room">The room to check against</param>
		public bool IsInRoom(Room room)
		{
			return CurrentRoom == room;
		}

		/// <summary>
		/// Check if this entity is in a particular room
		/// </summary>
		/// <param name="roomName">The name of the room to check against</param>
		public bool IsInRoom(string roomName)
		{
			return CurrentRoom.Name == roomName;
		}

		#endregion
	}
}
