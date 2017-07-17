using System;

namespace TagEngine.Entities
{
	/// <summary>
	/// An object that has a name and a description
	/// </summary>
	interface ILookable
	{
		/// <summary>
		/// Gets or sets a name
		/// </summary>
		string Title { get; set; }

		/// <summary>
		/// Gets or sets a description
		/// </summary>
		string Description { get; set; }

		/// <summary>
		/// Gets or sets an extended description or examination
		/// </summary>
		string ExtendedDescription { get; set; }
	}
}
