using Cafetown.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.DL
{
    public class InvoiceDL : BaseDL<Invoice>, IInvoiceDL
    {
        #region Field
        private readonly IConnectionDL _connectionDL;
        #endregion

        #region Constructor
        public InvoiceDL(IConnectionDL connectionDL) : base(connectionDL)
        {
            _connectionDL = connectionDL;
        }
        #endregion
    }
}
