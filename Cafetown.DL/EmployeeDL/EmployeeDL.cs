using Dapper;
using Cafetown.Common;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        #region Field
        private readonly IConnectionDL _connectionDL;
        #endregion

        #region Constructor
        public EmployeeDL(IConnectionDL connectionDL) : base(connectionDL)
        {
            _connectionDL = connectionDL;
        }
        #endregion

        public IEnumerable<Employee> ExportExcel(string? keyword)
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = string.Format(ProcedureNames.EXPORT_EXCEL, typeof(Employee).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("$Keyword", keyword); ;

            // Khai báo kết quả trả về
            var employees = default(IEnumerable<Employee>);

            // Khởi tạo kết nối đến DB
            using (var connection = _connectionDL.InitConnection(connectionString))
            {
                // Thực hiện gọi vào db
                employees = _connectionDL.Query<Employee>(connection, storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return employees;
        }

        /// <summary>
        /// Chức năng đăng nhập
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// TTTuan: 17/4/2023
        public Employee login(string username, string password)
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = string.Format(ProcedureNames.LOGIN, typeof(Employee).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("$Username", username);
            parameters.Add("$Password", password);

            Employee employee = new Employee();

            // Khởi tạo kết nối đến DB
            using (var connection = _connectionDL.InitConnection(connectionString))
            {
                employee = _connectionDL.QueryFirstOrDefault<Employee>(connection, storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return employee;
        }

        public int UpdateByEmail(string email)
        {
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = "Proc_Employee_UpdateByEmail";

            // Chuẩn bị tham số đầu vào cho procedure
            var parameters = new DynamicParameters();
            parameters.Add("$Email", email);

            var numberOfAffectedRows = 0;

            // Khởi tạo kết nối đến DB
            using (var connection = _connectionDL.InitConnection(connectionString))
            {
                // Gọi vào DB để chạy stored ở trên
                numberOfAffectedRows = _connectionDL.Execute(connection, storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return numberOfAffectedRows;
        }

        /// <summary>
        /// Sửa một bản ghi
        /// </summary>
        /// <param name="recordID"></param>
        /// <param name="record"></param>
        /// <returns>Trả về số dòng bị ảnh hưởng</returns>
        /// Modified by: TTTuan 5/1/2023
        public int UpdateRecordByLogin(Guid recordID, Employee record)
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = "Proc_Employee_UpdateByLogin";

            // Chuẩn bị tham số đầu vào cho procedure
            var parameters = new DynamicParameters();

            var properties = typeof(Employee).GetProperties();

            foreach (var property in properties)
            {
                var propertyName = property.Name;

                object? propertyValue;

                var primaryKeyAttribute = (PrimaryKeyAttribute?)Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute));
                if (primaryKeyAttribute != null)
                {
                    propertyValue = recordID;
                }
                else
                {
                    propertyValue = property.GetValue(record, null);
                }
                parameters.Add($"${propertyName}", propertyValue);
            }

            var numberOfAffectedRows = 0;

            // Khởi tạo kết nối đến DB
            using (var connection = _connectionDL.InitConnection(connectionString))
            {
                // Gọi vào DB để chạy stored ở trên
                numberOfAffectedRows = _connectionDL.Execute(connection, storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return numberOfAffectedRows;
        }
    }
}
