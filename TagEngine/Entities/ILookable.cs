using System;

namespace TagEngine.Entities
{
	/// <summary>
	/// An object that has a name and a description
	/// </summary>
	public interface ILookable
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

    /// <summary>
    /// Extension methods for ILookables
    /// </summary>
    public static class ILookableExtender
    {
        /// <summary>
        /// Describe an entity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string Describe(this ILookable item)
        {
            return item.Description;
        }

        /// <summary>
        /// Examine an entity
        /// </summary>
        /// <param name="item"></param>
        /// <param name="describeIfEmpty">If true, will return the short description if there is no extended description</param>
        /// <returns></returns>
        public static string Examine(this ILookable item, bool describeIfEmpty = false)
        {
            if (describeIfEmpty && String.IsNullOrWhiteSpace(item.ExtendedDescription)) return item.Description;

            return item.ExtendedDescription;
        }
    }
}
