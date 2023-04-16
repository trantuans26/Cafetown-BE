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
    }
}
