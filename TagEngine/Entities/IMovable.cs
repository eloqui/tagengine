using System;
using System.Collections.Generic;
using System.Text;

namespace TagEngine.Entities
{
	/// <summary>
	/// An entity that can have a position
	/// </summary>
	interface IMovable
	{
		/// <summary>
		/// Get the current room in which this entity is located
		/// </summary>
		Room CurrentRoom { get; }

		/// <summary>
		/// Move this entity to a new room
		/// </summary>
		/// <param name="room">The room to which to move the entity</param>
		void MoveTo(Room room);

		/// <summary>
		/// Move this entity to a new room
		/// </summary>
		/// <param name="room">The name of the room to which to move the entity</param>
		void MoveTo(string roomName);

		/// <summary>
		/// Check if this entity is in a particular room
		/// </summary>
		/// <param name="room">The room</param>
		/// <returns>True if this entity is in the room</returns>
		bool IsInRoom(Room room);

		/// <summary>
		/// Check if this entity is in a particular room
		/// </summary>
		/// <param name="roomName">The name of the room</param>
		/// <returns>True if this entity is in the room</returns>
		bool IsInRoom(string roomName);
	}
}
