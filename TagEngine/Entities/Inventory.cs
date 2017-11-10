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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TagEngine.Entities
{
	/// <summary>
	/// An inventory or collection of items
	/// </summary>
    [Serializable]
	public class Inventory : IEnumerable<Item>
	{
        /// <summary>
        /// The items in the inventory
        /// </summary>
        protected Dictionary<string, Item> items;

        /// <summary>
        /// The number of items in this inventory
        /// </summary>
        public int Count => items.Count;

        /// <summary>
        /// Get the total weight of carried items
        /// </summary>
        public int TotalWeight
        {
            get
            {
                int w = 0;
                foreach (var i in items)
                {
                    w += i.Value.Weight;
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
        public Inventory(int maxWeight)
        {
            items = new Dictionary<string, Item>();
            MaxWeight = maxWeight;
        }

        #region Methods

        /// <summary>
        /// Add an item to this inventory
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            items.Add(item.Name, item);
        }

        /// <summary>
        /// Remove an item from this inventory
        /// </summary>
        /// <param name="item"></param>
        public void RemoveItem(Item item)
        {
            if (!HasItem(item)) return;

            items.Remove(item.Name);
        }

        /// <summary>
        /// Check if this inventory has a particular item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool HasItem(Item item)
        {
            return items.ContainsValue(item);
        }

        /// <summary>
        /// Check if this inventory has a particular item name
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public bool HasItem(string itemName)
        {
            return items.ContainsKey(itemName);
        }

        /// <summary>
        /// Get enumerator for the items in this inventory
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Item> GetEnumerator()
        {
            return new ItemEnumerator(this);
        }
        IEnumerator GetEnumerator1()
        {
            return this.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator1();
        }

        #endregion

        internal class ItemEnumerator : IEnumerator<Item>
        {
            int position;
            Inventory inventory;
            Item currentItem;

            public Item Current => currentItem;
            object IEnumerator.Current => Current;

            public ItemEnumerator(Inventory inventory)
            {
                this.inventory = inventory;
                position = -1;
                currentItem = null;
            }

            public bool MoveNext()
            {
                if (++position >= inventory.items.Count) return false;

                currentItem = inventory.items.ElementAt(position).Value;

                return true;
            }

            public void Reset()
            {
                position = -1;
            }

            void IDisposable.Dispose() { }
        }
    }

}
