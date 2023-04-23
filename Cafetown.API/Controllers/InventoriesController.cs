using Cafetown.BL;
using Cafetown.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafetown.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class InventoriesController : BasesController<Inventory>
    {
        #region Field
        private readonly IInventoryBL _inventoryBL;
        #endregion

        #region Constructor
        public InventoriesController(IInventoryBL inventoryBL) : base(inventoryBL)
        {
            _inventoryBL = inventoryBL;
        }
        #endregion

        #region Method
        /// <summary>
        /// Xuất file excel danh sách hàng hóa
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns>File excel</returns>
        /// Modified by: TTTuan (5/3/2023)
        [HttpGet("export")]
        public IActionResult ExportExcel([FromQuery] string? keyword)
        {
            try
            {
                var stream = _inventoryBL.ExportExcel(keyword);
                string excelName = $"{ExcelResource.Export_Excel_FileName}_{DateTime.Now.ToString("ddMMyyyyHHmmss")}.xlsx";

                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
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
        #endregion
    }
}
