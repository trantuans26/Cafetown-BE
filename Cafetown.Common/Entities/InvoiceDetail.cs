using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.Common
{
    /// <summary>
    /// Chi tiết hóa đơn
    /// </summary>
    public class InvoiceDetail : BaseEntity
    {
        /// <summary>
        /// ID hóa đơn
        /// </summary>
        public Guid? InvoiceID { get; set; }

        /// <summary>
        /// Mã hóa đơn
        /// </summary>
        public string? InvoiceCode { get; set; }

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
        /// Đơn giá
        /// </summary>
        public float? Cost { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public int? Quantity { get; set; }

        /// <summary>
        /// Tổng tiền
        /// </summary>
        public float? TotalCost { get; set; }   



    }
}
