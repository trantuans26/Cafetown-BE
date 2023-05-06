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

        /// <summary>
        /// API Lấy danh sách thông tin bản ghi master detail
        /// </summary>
        /// <param name="keyword">Mã bản ghi, tên bản ghi, số điện thoại</param>
        /// <param name="pageSize">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Số chỉ mục của trang muốn lấy</param>
        /// <returns>Danh sách thông tin bản ghi & tổng số trang và tổng số bản ghi</returns>
        /// Created by: TTTuan (23/12/2022)
        [HttpGet("full")]
        public IActionResult GetMasterDetailByID([FromQuery] Guid invoiceID)
        {
            try
            {
                InvoiceMasterDetail invoiceMasterDetail = _invoiceBL.GetMasterDetailByID(invoiceID);

                return StatusCode(StatusCodes.Status200OK, invoiceMasterDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = ErrorResultResource.DevMsg_Exception,
                    UserMsg = ErrorResultResource.UserMsg_Exception,
                    MoreInfo = ErrorResultResource.MoreInfo_Exception,
                    TraceID = HttpContext.TraceIdentifier
                });
            }
        }

        [HttpPost("full")]
        public IActionResult InsertFull([FromBody] InvoiceMasterDetail request)
        {   
            try
            {
                int invoiceMasterDetail = _invoiceBL.InsertMasterDetail(request);

                return StatusCode(StatusCodes.Status201Created, invoiceMasterDetail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = ErrorResultResource.DevMsg_Exception,
                    UserMsg = ErrorResultResource.UserMsg_Exception,
                    MoreInfo = ErrorResultResource.MoreInfo_Exception,
                    TraceID = HttpContext.TraceIdentifier
                });
            }
        }

        [HttpPut("full/{id}")]
        public IActionResult UpdateFull([FromRoute] Guid id, [FromBody] InvoiceMasterDetail request)
        {
            try
            {
                var result = _invoiceBL.UpdateMasterDetail(id, request);

                // Xử lý kết quả trả về
                if (result > 0)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new
                    {
                        ErrorCode = ErrorCode.UpdateFailed,
                        DevMsg = ErrorResultResource.DevMsg_UpdateFailed,
                        UserMsg = ErrorResultResource.UserMsg_UpdateFailed,
                        MoreInfo = ErrorResultResource.MoreInfo_UpdateFailed,
                        TraceID = HttpContext.TraceIdentifier
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = ErrorResultResource.DevMsg_Exception,
                    UserMsg = ErrorResultResource.UserMsg_Exception,
                    MoreInfo = ErrorResultResource.MoreInfo_Exception,
                    TraceID = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// API Lấy danh sách thông tin bản ghi theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Mã bản ghi, tên bản ghi, số điện thoại</param>
        /// <param name="pageSize">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Số chỉ mục của trang muốn lấy</param>
        /// <returns>Danh sách thông tin bản ghi & tổng số trang và tổng số bản ghi</returns>
        /// Created by: TTTuan (23/12/2022)
        [HttpGet("invoiceFilter")]
        public IActionResult GetRecordsByFilter(
            [FromQuery] string? keyword,
            [FromQuery] int isCollected = 2,
            [FromQuery] int pageSize = 20,
            [FromQuery] int pageNumber = 1
        )
        {
            try
            {
                PagingResult<Invoice> recordFilter = _invoiceBL.GetInvoicesByFilter(keyword, isCollected, pageSize, pageNumber);

                return StatusCode(StatusCodes.Status200OK, recordFilter);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = ErrorResultResource.DevMsg_Exception,
                    UserMsg = ErrorResultResource.UserMsg_Exception,
                    MoreInfo = ErrorResultResource.MoreInfo_Exception,
                    TraceID = HttpContext.TraceIdentifier
                });
            }
        }
    }
}
