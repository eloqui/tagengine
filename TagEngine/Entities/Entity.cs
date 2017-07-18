using System;
using System.Collections.Generic;

namespace TagEngine.Entities
{
    /// <summary>
    /// An entity in the world
    /// </summary>
    [Serializable]
    public class Entity
    {
        /// <summary>
        /// Unique identifier for this object
        /// </summary>
        public Guid Id
        {
            get;
            protected set;
        }

        /// <summary>
        /// Internal name of this object
        /// </summary>
        public string Name
        {
            get;
            protected set;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of this entity</param>
        public Entity(string name)
        {
            this.Name = name;
            this.Id = Guid.NewGuid();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of this entity</param>
        /// <param name="id">The ID of this entity</param>
        public Entity(string name, Guid id)
        {
            this.Name = name;
            this.Id = id;
        }
    }

    /// <summary>
    /// A collection of entities indexed by a name
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Entities<T> : Dictionary<string, T>
        where T : Entity
    {

    }
}
