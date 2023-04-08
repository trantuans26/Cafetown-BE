using Cafetown.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.DL
{
    public interface IInventoryDL : IBaseDL<Inventory>
    {
        /// <summary>
        /// Xuất file excel
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        /// Modified by: TTTuan 5/1/2023
        public IEnumerable<Inventory> ExportExcel(string? keyword);
    }
}
