using System;

namespace TagEngine.Entities
{
	/// <summary>
	/// An entity that can be interacted with by the player
	/// </summary>
	[Serializable]
	public class InteractiveEntity : Entity, ILookable
	{
		#region Fields

		/// <summary>
		/// A name suitable for output
		/// </summary>
		string title;

		/// <summary>
		/// A short description of this object
		/// </summary>
		string description;

		/// <summary>
		/// An extended description of this object, used for examinations
		/// </summary>
		string extendedDescription = "";

		/// <summary>
		/// Whether this item can be seen by the user
		/// </summary>
		protected bool isAccessible = true;

		/// <summary>
		/// Whether this item can be examined by the user
		/// </summary>
		protected bool isExaminable = true;

		/// <summary>
		/// The entity's variables
		/// </summary>
		//protected Variables variables;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets whether this object can be accessed and carried by the user
		/// </summary>
		public virtual bool IsAccessible
		{
			get { return isAccessible; }
			set { isAccessible = value; }
		}

		/// <summary>
		/// Gets or sets whether this object can be examined by the user
		/// </summary>
		public virtual bool IsExaminable
		{
			get { return isExaminable; }
			set { isExaminable = value; }
		}

		/// <summary>
		/// Gets or sets the extended description of this object
		/// </summary>
		public virtual string ExtendedDescription
		{
			get { return extendedDescription; }
			set { extendedDescription = value; }
		}

		/// <summary>
		/// Gets or sets the short description of this object
		/// </summary>
		public virtual string Description
		{
			get { return description; }
			set { description = value; }
		}

		/// <summary>
		/// Gets or sets the title of this object
		/// </summary>
		public virtual string Title
		{
			get { return title; }
			set { title = value; }
		}

		/// <summary>
		/// Gets the entity's variables
		/// </summary>
		//public virtual Variables Variables
		//{
		//	get { return variables; }
		//}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The new object's name</param>
		/// <param name="title">The new object's title</param>
		/// <param name="description">The new object's description</param>
		public InteractiveEntity(string name, string title, string description)
			: base(name)
		{
			this.title = title;
			this.description = description;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The new object's name</param>
		/// <param name="title">The new object's title</param>
		/// <param name="description">The new object's description</param>
		/// <param name="isAccessible">The initial accessibility of the new object</param>
		public InteractiveEntity(string name, string title, string description, bool isAccessible)
			: base(name)
		{
			this.title = title;
			this.description = description;
			this.isAccessible = isAccessible;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The new object's name</param>
		/// <param name="title">The new object's title</param>
		/// <param name="description">The new object's description</param>
		/// <param name="extendedDescription">The new object's extended description</param>
		public InteractiveEntity(string name, string title, string description, string extendedDescription)
			: base(name)
		{
			this.title = title;
			this.description = description;
			this.extendedDescription = extendedDescription;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The new object's name</param>
		/// <param name="title">The new object's title</param>
		/// <param name="description">The new object's description</param>
		/// <param name="extendedDescription">The new object's extended description</param>
		/// <param name="isAccessible">The initial accessibility of the new object</param>
		public InteractiveEntity(string name, string title, string description, string extendedDescription, bool isAccessible)
			: base(name)
		{
			this.title = title;
			this.description = description;
			this.extendedDescription = extendedDescription;
			this.isAccessible = isAccessible;
		}

		#endregion
	}
}
