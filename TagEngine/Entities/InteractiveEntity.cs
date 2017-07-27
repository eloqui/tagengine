using System;

namespace TagEngine.Entities
{
	/// <summary>
	/// An entity that can be interacted with by the player
	/// </summary>
	[Serializable]
	public class InteractiveEntity : Entity, ILookable
	{
		#region Properties

        /// <summary>
        /// A name suitable for output
        /// </summary>
        public string Title { get; set; }

		/// <summary>
		/// The short description of this object
		/// </summary>
		public virtual string Description { get; set; }

        /// <summary>
        /// The extended description of this object
        /// </summary>
        public virtual string ExtendedDescription { get; set; } = "";

        /// <summary>
        /// Whether this object can be examined by the user
        /// </summary>
        public bool IsExaminable { get; set; } = true;

		/// <summary>
		/// Whether this object can be accessed and carried by the user
		/// </summary>
		public bool IsAccessible { get; set; }
        
		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The new object's name</param>
		/// <param name="title">The new object's title</param>
		/// <param name="description">The new object's description</param>
		public InteractiveEntity(string name, string title, string description, bool isAccessible = false)
			: base(name)
		{
			Title = title;
			Description = description;
            IsAccessible = isAccessible;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The new object's name</param>
		/// <param name="title">The new object's title</param>
		/// <param name="description">The new object's description</param>
		/// <param name="extendedDescription">The new object's extended description</param>
		public InteractiveEntity(string name, string title, string description, string extendedDescription, bool isAccessible = false)
			: this(name, title, description, false)
		{
			ExtendedDescription = extendedDescription;
            IsAccessible = isAccessible;
		}

        #endregion
    }
}
