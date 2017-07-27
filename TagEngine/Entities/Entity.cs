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
