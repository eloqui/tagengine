/*
 * MIT License
 * 
 * Copyright (c) 2017 Polarity Studio
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
 * documentation files (the "Software"), to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software,
 * and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions
 * of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
 * TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
 * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
 * CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
 * DEALINGS IN THE SOFTWARE.
 */

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
