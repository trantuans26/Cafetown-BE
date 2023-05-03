using Cafetown.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.DL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        /// <summary>
        /// Xuất file excel
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        /// Modified by: TTTuan 5/1/2023
        public IEnumerable<Employee> ExportExcel(string? keyword);
        
        /// <summary>
        /// Chức năng đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// TTTuan: 17/4/2023
        public Employee login(string username, string password);

        /// <summary>
        /// Sửa một bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <param name="record"></param>
        /// <returns>Trả về số dòng bị ảnh hưởng</returns>
        /// Modified by: TTTuan 5/1/2023
        public int UpdateRecordByLogin(Guid recordID, Employee record);
    }
}
