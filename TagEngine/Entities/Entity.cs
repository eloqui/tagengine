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
using System.Runtime.Serialization;

namespace TagEngine.Entities
{
    /// <summary>
    /// An entity in the world
    /// </summary>
    [Serializable]
    public class Entity
    {
        // TODO: add stats (e.g. strength, dex, int, vit etc.)
        // TODO: add xp and levels (probably in subclass)

        /// <summary>
        /// Unique identifier for this object
        /// </summary>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Internal name of this object
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of this entity</param>
        public Entity(string name) : this(name, Guid.NewGuid()) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of this entity</param>
        /// <param name="id">The ID of this entity</param>
        public Entity(string name, Guid id)
        {
            Name = name;
            Id = id;
        }
    }

    /// <summary>
    /// A collection of entities indexed by a name
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class Entities<T> : Dictionary<string, T>
        where T : Entity
    {
        public Entities() { }

        protected Entities(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
