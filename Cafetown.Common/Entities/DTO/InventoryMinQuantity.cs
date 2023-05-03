using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.Common
{
    public class InventoryMinQuantity
    {
        /// <summary>
        /// Nhỏ hơn 5
        /// </summary>
        public int? LessThanFive { get; set; }

        /// <summary>
        /// Bằng 0 
        /// </summary>
        public int? EqualZero { get; set; }
    }
}
