using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagEngine.Entities
{
    public interface IHasInventory
    {
        Inventory Inventory { get; }

        bool IsCarrying(Item item);
        bool IsCarrying(string itemName);
    }
}
