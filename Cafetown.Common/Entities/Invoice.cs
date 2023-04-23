using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.Common
{
    /// <summary>
    /// Hóa đơn
    /// </summary>
    public class Invoice : BaseEntity
    {
        /// <summary>
        /// ID hóa đơn
        /// </summary>
        [PrimaryKey]
        public Guid? InvoiceID { get; set; }

        /// <summary>
        /// Mã hóa đơn
        /// </summary>
        public string? InvoiceCode { get; set; }

        public string? Customer { get; set; }

        public string? PhoneCustomer { get; set; }

        /// <summary>
        /// Tổng tiền
        /// </summary>
        public float? TotalCost { get; set; }

        public string? Description { get; set; }

        /// <summary>
        /// Ngày mua
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        public bool? IsCollected { get; set; }
    }
}
