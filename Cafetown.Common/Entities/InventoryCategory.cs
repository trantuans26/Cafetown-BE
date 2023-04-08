using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.Common
{
    public class InventoryCategory : BaseEntity
    {
        /// <summary>
        /// ID danh mục hàng hóa
        /// </summary>
        [PrimaryKey]
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
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }

    }
}
