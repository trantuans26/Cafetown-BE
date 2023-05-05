using Cafetown.BL;
using Cafetown.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafetown.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DashboardsController : ControllerBase
    {
        #region Field
        private readonly IDashboardBL _dashboardBL;
        #endregion

        #region Constructor
        public DashboardsController(IDashboardBL dashboardBL)
        {
            _dashboardBL = dashboardBL;
        }
        #endregion

        /// <summary>
        /// API Lấy danh sách thông tin bản ghi theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Mã bản ghi, tên bản ghi, số điện thoại</param>
        /// <param name="pageSize">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Số chỉ mục của trang muốn lấy</param>
        /// <returns>Danh sách thông tin bản ghi & tổng số trang và tổng số bản ghi</returns>
        /// Created by: TTTuan (23/12/2022)
        [HttpGet("topInventory")]
        public IActionResult GetTopInventory()
        {
            try
            {
                TopInventoryResult result = _dashboardBL.GetTopInventory();

                return StatusCode(StatusCodes.Status200OK, result);
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
        [HttpGet("minQuantity")]
        public IActionResult GetInventoryByMinQuantity()
        {
            try
            {
                InventoryMinQuantity result = _dashboardBL.GetInventoryByMinQuantity();

                return StatusCode(StatusCodes.Status200OK, result);
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
        [HttpGet("sumTotalCosts")]
        public IActionResult GetSumTotalCosts()
        {
            try
            {
                SumTotalCosts result = _dashboardBL.GetSumTotalCosts();

                return StatusCode(StatusCodes.Status200OK, result);
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
        [HttpGet("topInvoice")]
        public IActionResult GetTopInvoice()
        {
            try
            {
                IEnumerable<Inventory> result = _dashboardBL.GetTopInvoice();

                return StatusCode(StatusCodes.Status200OK, result);
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
