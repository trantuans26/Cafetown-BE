using Aspose.Cells;
using Cafetown.Common;
using Cafetown.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.BL
{
    public class DashboardBL : IDashboardBL
    {
        #region Field
        private readonly IDashboardDL _dashboardDL;
        #endregion

        #region Constructor
        public DashboardBL(IDashboardDL dashboardDL)
        {
            _dashboardDL = dashboardDL;
        }
        #endregion

        public InventoryMinQuantity GetInventoryByMinQuantity()
        {
            return _dashboardDL.GetInventoryByMinQuantity();
        }

        public SumTotalCosts GetSumTotalCosts()
        {
            return new SumTotalCosts()
            {
                sumTotalCost = _dashboardDL.GetSumTotalCost(),
                sumTotalCostIsCollected = _dashboardDL.GetSumTotalCostByIsCollected(),
                sumTotalCostIsNotCollected = _dashboardDL.GetSumTotalCostByIsNotCollected()
            };
        }

        /// <summary>
        /// Lấy danh sách thông tin bản ghi theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Mã bản ghi, tên bản ghi, số điện thoại</param>
        /// <param name="pageSize">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Số chỉ mục của trang muốn lấy</param>
        /// <returns>Danh sách thông tin bản ghi & tổng số trang và tổng số bản ghi</returns>
        /// Created by: TTTuan (23/12/2022)
        public TopInventoryResult GetTopInventory()
        {
            return _dashboardDL.GetTopInventory();
        }

        public IEnumerable<Invoice> GetTopInvoice()
        {
            return _dashboardDL.GetTopInvoice();
        }
    }
}
