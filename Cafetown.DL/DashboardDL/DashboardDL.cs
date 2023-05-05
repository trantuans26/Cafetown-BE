using Cafetown.Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.DL
{
    public class DashboardDL : IDashboardDL
    {
        #region Field
        private readonly IConnectionDL _connectionDL;
        #endregion

        #region Constructor
        public DashboardDL(IConnectionDL connectionDL)
        {
            _connectionDL = connectionDL;
        }
        #endregion

        public InventoryMinQuantity GetInventoryByMinQuantity()
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = "Proc_Inventory_GetInventoryByMinQuantity";

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();

            // Khai báo kết quả trả về
            var lessThanFive = 0;
            var equalZero = 0;

            // Khởi tạo kết nối đến DB
            using (var mySqlConnection = _connectionDL.InitConnection(connectionString))
            {
                // Gọi vào DB để chạy stored ở trên
                var records = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                lessThanFive = records.Read<int>().FirstOrDefault();
                equalZero = records.Read<int>().LastOrDefault();
            }

            return new InventoryMinQuantity()
            {
                LessThanFive = lessThanFive,
                EqualZero = equalZero
            };
        }

        public float GetSumTotalCost()
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = "Proc_Invoice_GetSumTotalCost";

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();

            // Khai báo kết quả trả về
            var result = 0.0f;

            // Khởi tạo kết nối đến DB
            using (var mySqlConnection = _connectionDL.InitConnection(connectionString))
            {
                // Gọi vào DB để chạy stored ở trên
                var records = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                result = records.Read<float>().FirstOrDefault();
            }

            return result;
        }

        public float GetSumTotalCostByIsCollected()
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = "Proc_Invoice_GetSumTotalCostByIsCollected";

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();

            // Khai báo kết quả trả về
            var result = 0.0f;

            // Khởi tạo kết nối đến DB
            using (var mySqlConnection = _connectionDL.InitConnection(connectionString))
            {
                // Gọi vào DB để chạy stored ở trên
                var records = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                result = records.Read<float>().FirstOrDefault();
            }

            return result;
        }

        public float GetSumTotalCostByIsNotCollected()
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = "Proc_Invoice_GetSumTotalCostByIsNotCollected";

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();

            // Khai báo kết quả trả về
            var result = 0.0f;

            // Khởi tạo kết nối đến DB
            using (var mySqlConnection = _connectionDL.InitConnection(connectionString))
            {
                // Gọi vào DB để chạy stored ở trên
                var records = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                result = records.Read<float>().FirstOrDefault();
            }

            return result;
        }

        public TopInventoryResult GetTopInventory()
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = "Proc_Inventory_GetTopValue";

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();

            // Khai báo kết quả trả về
            var list = new List<Inventory>();
            var total = 0;

            // Khởi tạo kết nối đến DB
            using (var mySqlConnection = _connectionDL.InitConnection(connectionString))
            {
                // Gọi vào DB để chạy stored ở trên
                var records = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                total = records.Read<int>().FirstOrDefault();
                list = records.Read<Inventory>().ToList();
            }

            return new TopInventoryResult()
            {
                Data = list,
                Total = total
            };
        }

        public IEnumerable<Inventory> GetTopInvoice()
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = "Proc_Invoice_GetTopInvoice";

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();

            // Khai báo kết quả trả về
            var records = default(IEnumerable<Inventory>);

            // Khởi tạo kết nối đến DB
            using (var mySqlConnection = _connectionDL.InitConnection(connectionString))
            {
                // Gọi vào DB để chạy stored ở trên
               records = mySqlConnection.Query<Inventory>(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);
            }

            return records;
        }
    }
}
