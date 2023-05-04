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
        #endregion
    }
}
