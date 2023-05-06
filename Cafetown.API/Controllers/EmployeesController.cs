using Cafetown.BL;
using Cafetown.Common;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Cafetown.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BasesController<Employee>
    {
        #region Field
        private readonly IEmployeeBL _employeeBL;
        #endregion

        #region Constructor
        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }
        #endregion

        #region Method
        /// <summary>
        /// Xuất file excel danh sách nhân viên
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns>File excel</returns>
        /// Modified by: TTTuan (5/3/2023)
        [HttpGet("export")]
        public IActionResult ExportExcel([FromQuery] string? keyword)
        {
            try
            {
                var stream = _employeeBL.ExportExcel(keyword);
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

        /// <summary>
        /// Chức năng đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// TTTuan: 17/4/2023
        [HttpGet("login")]
        public IActionResult login([FromQuery] string username, [FromQuery] string password)
        {
            try
            {
                var employee = _employeeBL.login(username, password);

                if (employee != null)
                {
                    return StatusCode(StatusCodes.Status200OK, employee);
                } 
                else 
                {
                    return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                    {
                        ErrorCode = ErrorCode.InvalidInput,
                        DevMsg = ErrorResultResource.DevMsg_InvalidInput,
                        UserMsg = ErrorResultResource.UserMsg_InvalidInput,
                        MoreInfo = HttpContext.TraceIdentifier
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
        [HttpPut("login/{id}")]
        public IActionResult UpdateRecordByLogin([FromRoute] Guid id, [FromBody] Employee record)
        {
            try
            {
                var result = _employeeBL.UpdateRecordByLogin(id, record);

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
        /// API Sửa 1 bản ghi theo ID
        /// </summary>
        /// <param name="id">ID của bản ghi cần sửa</param>
        /// <param name="record">Đối tượng bản ghi cần sửa</param>
        /// <returns>ID của bản ghi vừa sửa</returns>
        /// Created by: TTTuan (23/12/2022)
        [HttpPost("sendMail/{mail}")]
        public IActionResult SendEmail([FromRoute] string mail)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("natalia90@ethereal.email"));
                email.To.Add(MailboxAddress.Parse("trantuandev26@gmail.com"));
                email.Subject = "Mật khẩu tài khoản quản lý cửa hàng cafe Thái Tuấn của bạn đã được reset";
                email.Body = new TextPart(TextFormat.Html)
                {
                    Text = "Mật khẩu hiện tại của bạn là 12345678, vui lòng đổi mật khẩu sau khi đăng nhập lại!"
                };

                using var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                //smtp.Authenticate("natalia90@ethereal.email", "6hDyRCSvuqSDTEkDyz");
                smtp.Send(email);
                smtp.Disconnect(true);

                return StatusCode(StatusCodes.Status200OK);
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
