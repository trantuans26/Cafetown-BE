using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.Common
{
    public class Inventory : BaseEntity
    {
        /// <summary>
        /// ID hàng hóa
        /// </summary>
        public Guid? InventoryID { get; set; }

        /// <summary>
        /// Mã hàng hóa
        /// </summary>
        public string? InventoryCode { get; set; }

        /// <summary>
        /// Tên hàng hóa
        /// </summary>
        public string? InventoryName { get; set; }

        /// <summary>
        /// Danh mục hàng hóa
        /// </summary>
        public Guid? InventoryCategoryID { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int? Quantity { get; set; }    

        /// <summary>
        /// Đơn giá
        /// </summary>
        public int? Cost { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
    }
}
