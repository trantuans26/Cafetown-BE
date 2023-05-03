using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.Common
{
    public class TopInventoryResult
    {
        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        public List<Inventory>? Data { get; set; }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public long? Total { get; set; }
    }
}
