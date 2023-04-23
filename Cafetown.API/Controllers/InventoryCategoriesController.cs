using Cafetown.BL;
using Cafetown.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafetown.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InventoryCategoriesController : ControllerBase
    {
        #region Field
        private readonly IInventoryCategoryBL _inventoryCategoryBL;
        #endregion

        #region Constructor
        public InventoryCategoriesController(IInventoryCategoryBL inventoryCategoryBL)
        {
            _inventoryCategoryBL = inventoryCategoryBL;
        }
        #endregion

        /// <summary>
        /// API lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi trong bảng</returns>
        /// Modified by: TTTuan 5/3/2023
        [HttpGet]
        public IActionResult GetAllRecords()
        {
            try
            {   // khai báo biến hứng record
                var records = _inventoryCategoryBL.GetAllRecords();

                // Trả về status code và record
                return StatusCode(StatusCodes.Status200OK, records);
            }
            catch (Exception)
            {
                // Trả về status code và thông báo nếu lỗi
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
