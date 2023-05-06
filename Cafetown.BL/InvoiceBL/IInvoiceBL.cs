using Cafetown.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.BL
{
    public interface IInvoiceBL : IBaseBL<Invoice>
    {
        /// Created by: TTTuan (23/12/2022)
        public InvoiceMasterDetail GetMasterDetailByID(Guid InvoiceID);

        public int InsertMasterDetail(InvoiceMasterDetail request);

        public int UpdateMasterDetail(Guid invoiceID, InvoiceMasterDetail request);

        public PagingResult<Invoice> GetInvoicesByFilter(string? keyword, int isCollected, int pageSize, int pageNumber);
    }
}
