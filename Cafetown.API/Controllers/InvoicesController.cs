using Cafetown.BL;
using Cafetown.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafetown.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InvoicesController : BasesController<Invoice>
    {
        #region Field
        private readonly IInvoiceBL _invoiceBL;
        #endregion

        #region Constructor
        public InvoicesController(IInvoiceBL invoiceBL) : base(invoiceBL)
        {
            _invoiceBL = invoiceBL;
        }
        #endregion
    }
}
