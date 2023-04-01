using Cafetown.BL;
using Cafetown.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafetown.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field
        private readonly IBaseBL<T> _baseBL;
        #endregion

        #region Constructor
        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }
        #endregion

        #region Method
        /// <summary>
        /// API lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi trong bảng</returns>
        /// Modified by: TTTuan 5/1/2023
        [HttpGet]
        public IActionResult GetAllRecords()
        {
            try
            {   // khai báo biến hứng record
                var records = _baseBL.GetAllRecords();

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

        /// <summary>
        /// API Lấy danh sách thông tin bản ghi theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Mã bản ghi, tên bản ghi, số điện thoại</param>
        /// <param name="pageSize">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Số chỉ mục của trang muốn lấy</param>
        /// <returns>Danh sách thông tin bản ghi & tổng số trang và tổng số bản ghi</returns>
        /// Created by: TTTuan (23/12/2022)
        [HttpGet("filter")]
        public IActionResult GetRecordsByFilter(
            [FromQuery] string? keyword,
            [FromQuery] int pageSize = 20,
            [FromQuery] int pageNumber = 1
        )
        {
            try
            {
                PagingResult<T> recordFilter = _baseBL.GetRecordsByFilter(keyword, pageSize, pageNumber);

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

        /// <summary>
        /// API lấy thông tin chi tiết của 1 bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi</param>
        /// <returns>Thông tin của bản ghi theo ID</returns>
        /// Modified by: TTTuan 5/1/2023
        [HttpGet("{id}")]
        public IActionResult GetRecordByID([FromRoute] Guid id)
        {
            try
            {
                // khai báo biến hứng record
                var record = _baseBL.GetRecordByID(id);

                // Trả về status code và record
                return StatusCode(StatusCodes.Status200OK, record);
            }
            catch (Exception)
            {
                // Trả về status code và thông báo nếu lỗi
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(
                    ErrorCode.Exception,
                    ErrorResultResource.DevMsg_Exception,
                    ErrorResultResource.UserMsg_Exception,
                    ErrorResultResource.MoreInfo_Exception,
                    HttpContext.TraceIdentifier
                    ));
            }
        }

        /// <summary>
        /// API Lấy mã mới
        /// </summary>
        /// <returns>Mã mới</returns>
        /// Created by: TTTuan (23/12/2022)
        [HttpGet("newCode")]
        public IActionResult GetNewCode()
        {
            try
            {
                var newCode = _baseBL.GetNewCode();

                // Xử lý kết quả trả về
                if (newCode != null)
                {
                    return StatusCode(StatusCodes.Status200OK, newCode);
                }
                return StatusCode(StatusCodes.Status404NotFound);
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
        /// API Thêm mới 1 bản ghi
        /// </summary>
        /// <param name="newRecord">Đối tượng bản ghi cần thêm mới</param>
        /// <returns>ID của bản ghi vừa thêm mới</returns>
        /// Created by: TTTuan (23/12/2022)
        [HttpPost]
        public IActionResult InsertRecord([FromBody] T newRecord)
        {
            try
            {
                var result = _baseBL.InsertRecord(newRecord);

                // Xử lý kết quả trả về
                if (result.StatusResponse == StatusResponse.Done)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else if (result.StatusResponse == StatusResponse.Invalid || result.StatusResponse == StatusResponse.DuplicateCode)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.Data);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult()
                    {
                        ErrorCode = ErrorCode.InsertFailed,
                        DevMsg = ErrorResultResource.DevMsg_InsertFailed,
                        UserMsg = ErrorResultResource.UserMsg_InsertFailed,
                        MoreInfo = ErrorResultResource.MoreInfo_InsertFailed,
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
        /// API Sửa 1 bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần sửa</param>
        /// <param name="record">Đối tượng bản ghi cần sửa</param>
        /// <returns>ID của bản ghi vừa sửa</returns>
        /// Created by: TTTuan (23/12/2022)
        [HttpPut("{id}")]
        public IActionResult UpdateRecordByID([FromRoute] Guid id, [FromBody] T record)
        {
            try
            {
                var result = _baseBL.UpdateRecordByID(id, record);

                // Xử lý kết quả trả về
                if (result.StatusResponse == StatusResponse.Done)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else if (result.StatusResponse == StatusResponse.Invalid || result.StatusResponse == StatusResponse.DuplicateCode)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result.Data);
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
        /// API Xóa 1 bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần xóa</param>
        /// <returns>ID của bản ghi vừa xóa</returns>
        /// Created by: TTTuan (23/12/2022)
        [HttpDelete("{id}")]
        public IActionResult DeleteRecordByID([FromRoute] Guid id)
        {
            try
            {
                // Xử lý kết quả trả về
                if (_baseBL.DeleteRecordByID(id) > 0)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new
                    {
                        ErrorCode = ErrorCode.DeleteFailed,
                        DevMsg = ErrorResultResource.DevMsg_DeleteFailed,
                        UserMsg = ErrorResultResource.UserMsg_DeleteFailed,
                        MoreInfo = ErrorResultResource.MoreInfo_DeleteFailed,
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
        /// Xoá nhiều bản ghi
        /// </summary>
        /// <param name="listIDs"></param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// Modified by: TTTuan 5/1/2023
        [HttpPost("deleteBatch")]
        public IActionResult DeleteRecordsByIDs([FromBody] ListIDs listIDs)
        {
            try
            {
                var numberOfAffectedRows = _baseBL.DeleteRecordsByIDs(listIDs.IDs);
                if (numberOfAffectedRows > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, listIDs.IDs);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, new
                    {
                        ErrorCode = ErrorCode.DeleteFailed,
                        DevMsg = ErrorResultResource.DevMsg_DeleteFailed,
                        UserMsg = ErrorResultResource.UserMsg_DeleteFailed,
                        MoreInfo = ErrorResultResource.MoreInfo_DeleteFailed,
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
        #endregion
    }
}
