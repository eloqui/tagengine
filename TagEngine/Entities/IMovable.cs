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
		/// <param name="roomName">The name of the room to which to move the entity</param>
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
