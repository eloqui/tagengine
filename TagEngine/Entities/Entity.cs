using System;
using System.Collections.Generic;
using System.Text;

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
		private Guid id;

		/// <summary>
		/// An internal reference name for this object
		/// </summary>
		private string name;

		/// <summary>
		/// Gets the unique identifier for this object
		/// </summary>
		public virtual Guid Id
		{
			get { return id; }
		}

		/// <summary>
		/// Gets the internal name of this object
		/// </summary>
		public virtual string Name
		{
			get { return name; }
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The name of this entity</param>
		public Entity(string name)
		{
			this.name = name;
			this.id = Guid.NewGuid();
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The name of this entity</param>
		/// <param name="id">The ID of this entity</param>
		public Entity(string name, Guid id)
		{
			this.name = name;
			this.id = id;
		}
	}
}
