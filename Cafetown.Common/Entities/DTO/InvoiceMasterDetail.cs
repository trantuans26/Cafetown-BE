using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.Common
{
    public class InvoiceMasterDetail
    {
        public Invoice? InvoiceMaster { get; set; }

        public List<InvoiceDetail>? InvoiceDetails { get; set; }
    }
}
