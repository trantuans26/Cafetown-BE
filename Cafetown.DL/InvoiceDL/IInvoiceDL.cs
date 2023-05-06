using Cafetown.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.DL
{
    public interface IInvoiceDL : IBaseDL<Invoice>
    {
        /// Created by: TTTuan (23/12/2022)
        public InvoiceMasterDetail GetMasterDetailByID(Guid InvoiceID);

        public int ResetInvoiceDetailsByID(Guid InvoiceID);

        public int InsertDetail(Guid InvoiceID, InvoiceDetail InvoiceDetail);

        public int InsertMaster(Invoice invoice);

        public int ResetInventoryByID(Guid inventoryID, Guid invoiceID);

        public PagingResult<Invoice> GetInvoicesByFilter(string? keyword, int isCollected, int pageSize, int pageNumber);

    }
}
