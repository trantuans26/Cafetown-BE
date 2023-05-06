using Cafetown.Common;
using Cafetown.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.BL
{
    public class InvoiceBL : BaseBL<Invoice>, IInvoiceBL
    {
        #region Field
        private readonly IInvoiceDL _invoiceDL;
        #endregion

        #region Constructor
        public InvoiceBL(IInvoiceDL invoiceDL) : base(invoiceDL)
        {
            _invoiceDL = invoiceDL;
        }
        #endregion
        public InvoiceMasterDetail GetMasterDetailByID(Guid InvoiceID)
        {
            return _invoiceDL.GetMasterDetailByID(InvoiceID);
        }

        public int InsertMasterDetail(InvoiceMasterDetail request)
        {
            var master = request.InvoiceMaster;
            var details = request.InvoiceDetails;
            var detailsResult = 0;
            var result = 0;
            
            if(master != null)
            {
                master.InvoiceID = Guid.NewGuid();

                result = _invoiceDL.InsertMaster(master);

                if (details != null && details.Count > 0)
                {
                    foreach (InvoiceDetail detail in details)
                    {
                        detailsResult += _invoiceDL.InsertDetail(master.InvoiceID.HasValue ? master.InvoiceID.Value : Guid.Empty, detail);
                    }
                }
            }

            return result;
        }

        public int UpdateMasterDetail(Guid invoiceID, InvoiceMasterDetail request)
        {
            var master = request.InvoiceMaster;
            var details = request.InvoiceDetails;
            var detailsResult = 0;
            var result = 0;

            if (master != null)
            {
                result = _invoiceDL.UpdateRecordByID(invoiceID, master);

                if (details != null && details.Count > 0)
                {
                    foreach (InvoiceDetail detail in details)
                    {
                        detailsResult += _invoiceDL.ResetInventoryByID(detail.InventoryID.HasValue ? detail.InventoryID.Value : Guid.Empty, invoiceID);
                    }
                }

                _invoiceDL.ResetInvoiceDetailsByID(invoiceID);

                if (details != null && details.Count > 0)
                {
                    foreach (InvoiceDetail detail in details)
                    {
                        detailsResult += _invoiceDL.InsertDetail(invoiceID, detail);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Lấy danh sách thông tin bản ghi theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Mã bản ghi, tên bản ghi, số điện thoại</param>
        /// <param name="pageSize">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Số chỉ mục của trang muốn lấy</param>
        /// <returns>Danh sách thông tin bản ghi & tổng số trang và tổng số bản ghi</returns>
        /// Created by: TTTuan (23/12/2022)
        public PagingResult<Invoice> GetInvoicesByFilter(string? keyword, int isCollected, int pageSize, int pageNumber)
        {
            return _invoiceDL.GetInvoicesByFilter(keyword, isCollected, pageSize, pageNumber);
        }
    }
}
