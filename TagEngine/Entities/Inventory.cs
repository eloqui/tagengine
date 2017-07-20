using System;
using System.Collections.Generic;
using System.Text;

namespace TagEngine.Entities
{
	/// <summary>
	/// An inventory or collection of items
	/// </summary>
    [Serializable]
	public class Inventory : List<Item>
	{
        /// <summary>
        /// Get the total weight of carried items
        /// </summary>
        public int TotalWeight
        {
            get
            {
                int w = 0;
                foreach (var i in this)
                {
                    w += i.Weight;
                }
                return w;
            }
        }

        /// <summary>
        /// Total weight this inventory can support
        /// </summary>
        public int MaxWeight { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxWeight"></param>
        public Inventory(int maxWeight) : base()
        {
            MaxWeight = maxWeight;
        }

        #region Methods



        #endregion
    }
}
