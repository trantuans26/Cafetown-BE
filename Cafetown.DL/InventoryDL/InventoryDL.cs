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
    public class InventoryDL : BaseDL<Inventory>, IInventoryDL
    {
        #region Field
        private readonly IConnectionDL _connectionDL;
        #endregion

        #region Constructor
        public InventoryDL(IConnectionDL connectionDL) : base(connectionDL)
        {
            _connectionDL = connectionDL;
        }
        #endregion

        public IEnumerable<Inventory> ExportExcel(string? keyword)
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = string.Format(ProcedureNames.EXPORT_EXCEL, typeof(Inventory).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("$Keyword", keyword); ;

            // Khai báo kết quả trả về
            var inventories = default(IEnumerable<Inventory>);

            // Khởi tạo kết nối đến DB
            using (var connection = _connectionDL.InitConnection(connectionString))
            {
                // Thực hiện gọi vào db
                inventories = _connectionDL.Query<Inventory>(connection, storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return inventories;
        }
    }
}
