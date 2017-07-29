using System;
using System.Collections.Generic;
using System.Linq;
using TagEngine.Scripting;

namespace TagEngine.Entities
{
	/// <summary>
	/// An entity that can be interacted with by the player
	/// </summary>
	[Serializable]
	public abstract class InteractiveEntity : Entity, ILookable
	{
        #region Properties & Fields

        /// <summary>
        /// The short description of this entity
        /// </summary>
        protected string description;

        /// <summary>
        /// The extended description of this entity
        /// </summary>
        protected string extendedDescription = "";

        /// <summary>
        /// A name suitable for output
        /// </summary>
        public string Title { get; set; }

		/// <summary>
		/// The short description of this object
		/// </summary>
        public virtual string Description { 
            get { return description; } 
            set { description = value; }
        }

        /// <summary>
        /// The extended description of this object
        /// </summary>
        public virtual string ExtendedDescription {
            get { return extendedDescription; }
            set { extendedDescription = value; }
        }

        /// <summary>
        /// Whether this object can be examined by the user
        /// </summary>
        public bool IsExaminable { get; set; } = true;

		/// <summary>
		/// Whether this object can be accessed and carried by the user
		/// </summary>
		public bool IsAccessible { get; set; }

        /// <summary>
        /// Dialogues for this entity
        /// </summary>
        public List<Dialogue> Dialogues { get; protected set; }

        protected int currentDialogue = 0;

        public Dialogue DefaultDialogue { get; protected set; }
        
		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The new object's name</param>
		/// <param name="title">The new object's title</param>
		/// <param name="description">The new object's description</param>
		protected InteractiveEntity(string name, string title, string description, bool isAccessible = false)
			: base(name)
		{
			Title = title;
			this.description = description;
            IsAccessible = isAccessible;
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="name">The new object's name</param>
		/// <param name="title">The new object's title</param>
		/// <param name="description">The new object's description</param>
		/// <param name="extendedDescription">The new object's extended description</param>
		protected InteractiveEntity(string name, string title, string description, string extendedDescription, bool isAccessible = false)
			: this(name, title, description, false)
		{
			this.extendedDescription = extendedDescription;
            IsAccessible = isAccessible;
		}

        #endregion

        /// <summary>
        /// Add a line of dialogue
        /// </summary>
        /// <param name="d"></param>
        public void AddDialogue(Dialogue d)
        {
            Dialogues.Add(d);
        }

        /// <summary>
        /// Get dialogue at an index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Dialogue GetDialogue(int index)
        {
            if (index >= Dialogues.Count) index = Dialogues.Count - 1;
            if (index < 0) index = 0;

            return Dialogues[index];
        }

        /// <summary>
        /// Get dialogue by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Dialogue GetDialogue(string name)
        {
            return Dialogues.FirstOrDefault(d => d.Name == name);
        }

        /// <summary>
        /// Get the current dialogue
        /// </summary>
        /// <returns></returns>
        public Dialogue GetCurrentDialogue()
        {
            return GetDialogue(currentDialogue);
        }

        /// <summary>
        /// Get the next dialogue
        /// </summary>
        /// <returns></returns>
        public Dialogue GetNextDialogue()
        {
            currentDialogue++;
            return GetCurrentDialogue();
        }
    }
}
