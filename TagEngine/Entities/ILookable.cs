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
