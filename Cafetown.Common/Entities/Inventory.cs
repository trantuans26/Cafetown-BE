using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;

namespace Cafetown.Common
{
    /// <summary>
    /// Hàng hóa
    /// </summary>
    public class Inventory : BaseEntity
    {
        /// <summary>
        /// ID hàng hóa
        /// </summary>
        [PrimaryKey]
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
        /// Mã danh mục hàng hóa
        /// </summary>
        public string? InventoryCategoryCode { get; set; }

        /// <summary>
        /// Tên danh mục hàng hóa
        /// </summary>
        public string? InventoryCategoryName { get; set; }

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

        public string? Image 
        { 
            get; 
            set; 
        }
    }
}
