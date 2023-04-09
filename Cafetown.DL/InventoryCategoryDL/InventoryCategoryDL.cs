using Cafetown.Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.DL
{
    public class InventoryCategoryDL : IInventoryCategoryDL
    {
        #region Field
        private readonly IConnectionDL _connectionDL;
        #endregion

        #region Constructor
        public InventoryCategoryDL(IConnectionDL connectionDL)
        {
            _connectionDL = connectionDL;
        }
        #endregion

        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách toàn bộ bản ghi trong bảng</returns>
        /// Modified by: TTTuan 5/1/2023 
        public IEnumerable<InventoryCategory> GetAllRecords()
        {
            //khai báo tên stored procedure
            var storedProcedure = String.Format(ProcedureNames.GET_ALL, typeof(InventoryCategory).Name);

            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tham số đầu
            var parameters = new DynamicParameters();

            // Khai báo kết quả trả về
            var records = default(IEnumerable<InventoryCategory>);

            // Khởi tạo kết nối đến DB
            using (var connection = _connectionDL.InitConnection(connectionString))
            {
                // Thực hiện gọi vào db
                records = _connectionDL.Query<InventoryCategory>(connection, storedProcedure, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return records;
        }
    }
}
