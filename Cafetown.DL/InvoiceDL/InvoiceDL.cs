using Cafetown.Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.DL
{
    public class InvoiceDL : BaseDL<Invoice>, IInvoiceDL
    {
        #region Field
        private readonly IConnectionDL _connectionDL;
        #endregion

        #region Constructor
        public InvoiceDL(IConnectionDL connectionDL) : base(connectionDL)
        {
            _connectionDL = connectionDL;
        }

        #endregion

        #region Method
        public InvoiceMasterDetail GetMasterDetailByID(Guid InvoiceID)
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = "Proc_Invoice_GetMasterDetailByID";

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("$InvoiceID", InvoiceID);

            // Khai báo kết quả trả về
            var invoiceMasterDetail = new InvoiceMasterDetail();

            // Khởi tạo kết nối đến DB
            using (var mySqlConnection = _connectionDL.InitConnection(connectionString))
            {
                // Gọi vào DB để chạy stored ở trên
                var records = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                invoiceMasterDetail.InvoiceMaster = records.Read<Invoice>().FirstOrDefault();
                invoiceMasterDetail.InvoiceDetails = records.Read<InvoiceDetail>().ToList();
            }

            return invoiceMasterDetail;
        }

        public int InsertDetail(Guid InvoiceID, InvoiceDetail InvoiceDetail)
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = string.Format(ProcedureNames.INSERT, typeof(InvoiceDetail).Name);

            // Chuẩn bị tham số đầu vào cho procedure
            var parameters = new DynamicParameters();

            var newID = Guid.NewGuid();

            var properties = typeof(InvoiceDetail).GetProperties();

            foreach (var property in properties)
            {
                var propertyName = property.Name;

                object? propertyValue;

                var primaryKeyAttribute = (PrimaryKeyAttribute?)Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute));

                if (primaryKeyAttribute != null)
                {
                    propertyValue = InvoiceID;
                }
                else
                {
                    propertyValue = property.GetValue(InvoiceDetail, null);
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

        public int InsertMaster(Invoice invoice)
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = string.Format(ProcedureNames.INSERT, typeof(Invoice).Name);

            // Chuẩn bị tham số đầu vào cho procedure
            var parameters = new DynamicParameters();

            var properties = typeof(Invoice).GetProperties();

            foreach (var property in properties)
            {
                var propertyName = property.Name;

                object? propertyValue;

                var primaryKeyAttribute = (PrimaryKeyAttribute?)Attribute.GetCustomAttribute(property, typeof(PrimaryKeyAttribute));

                if (primaryKeyAttribute != null)
                {
                    propertyValue = invoice.InvoiceID;
                }
                else
                {
                    propertyValue = property.GetValue(invoice, null);
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

        public int ResetInventoryByID(Guid inventoryID, Guid invoiceID)
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = "Proc_Inventory_ResetInventoryByID";

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("$InvoiceID", invoiceID);
            parameters.Add("$InventoryID", inventoryID);

            // Khai báo kết quả trả về
            var afftectedRows = 0;

            // Khởi tạo kết nối đến DB
            using (var mySqlConnection = _connectionDL.InitConnection(connectionString))
            {
                // Gọi vào DB để chạy stored ở trên
                afftectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            }

            return afftectedRows;
        }

        public int ResetInvoiceDetailsByID(Guid InvoiceID)
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = "Proc_Invoice_ResetDetailsByID";

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("$InvoiceID", InvoiceID);

            // Khai báo kết quả trả về
            var afftectedRows = 0;

            // Khởi tạo kết nối đến DB
            using (var mySqlConnection = _connectionDL.InitConnection(connectionString))
            {
                // Gọi vào DB để chạy stored ở trên
                afftectedRows = mySqlConnection.Execute(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

            }

            return afftectedRows;
        }

        /// <summary>
        /// Lấy danh sách thông tin bản ghi theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Mã bản ghi, tên bản ghi, số điện thoại</param>
        /// <param name="pageSize">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Số chỉ mục của trang muốn lấy</param>
        /// <returns>Danh sách thông tin bản ghi & tổng số trang và tổng số bản ghi</returns>
        /// Created by: TTTuan (23/12/2022)
        public PagingResult<Invoice> GetInvoicesByFilter(string? keyword, int isCollected, int pageSize, int pageNumber)
        {
            // Chuẩn bị chuỗi kết nối
            var connectionString = DataContext.ConnectionString;

            // Chuẩn bị tên stored procedure
            var storedProcedureName = string.Format(ProcedureNames.GET_BY_FILTER, typeof(Invoice).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("$Keyword", keyword);
            parameters.Add("$IsCollected", isCollected);
            parameters.Add("$PageSize", pageSize);
            parameters.Add("$PageNumber", pageNumber);

            // Khai báo kết quả trả về
            var count = 0;
            var list = new List<Invoice>();
            var totalPage = 0;

            // Khởi tạo kết nối đến DB
            using (var mySqlConnection = _connectionDL.InitConnection(connectionString))
            {
                // Gọi vào DB để chạy stored ở trên
                var records = mySqlConnection.QueryMultiple(storedProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                count = records.Read<int>().FirstOrDefault();
                list = records.Read<Invoice>().ToList();
                totalPage = (int)Math.Ceiling((double)count / pageSize);
            }

            return new PagingResult<Invoice>
            {
                Data = list,
                TotalRecord = count,
                TotalPage = totalPage
            };
        }
        #endregion
    }
}
