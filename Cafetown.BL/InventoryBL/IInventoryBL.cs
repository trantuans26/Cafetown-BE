using Cafetown.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.BL
{
    public interface IInventoryBL : IBaseBL<Inventory>
    {
        /// <summary>
        /// Kiểm tra mã trùng
        /// </summary>
        /// <param name="inventory"></param>
        /// <param name="inventoryID"></param>
        /// <returns>bool kiểm tra có trùng hay không</returns>
        /// Modified by: TTTuan 5/1/2023
        public new ServiceResponse CheckDuplicateCode(Guid? inventoryID, Inventory inventory);

        /// <summary>
        /// Xuất file excel danh sách hàng hóa
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns>File excel</returns>
        /// Modified by: TTTuan (5/1/2022)
        public MemoryStream ExportExcel(string? keyword);

        /// <summary>
        /// Validate dữ liệu đầu vào
        /// </summary>
        /// <param name="inventory"></param>
        /// <returns>Đối tượng ServiceResponse mỗ tả thành công hay thất bại</returns>
        /// Created by: TTTuan (23/12/2022)
        public new ServiceResponse ValidateData(Inventory inventory);
    }
}
