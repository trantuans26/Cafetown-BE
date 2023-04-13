using Cafetown.Common;
using Cafetown.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafetown.BL
{
    public class InventoryCategoryBL : IInventoryCategoryBL
    {
        #region Field
        private readonly IInventoryCategoryDL _inventoryCategoryDL;
        #endregion

        #region Constructor
        public InventoryCategoryBL(IInventoryCategoryDL inventoryCategoryDL) 
        {
            _inventoryCategoryDL = inventoryCategoryDL;
        }
        #endregion

        public IEnumerable<InventoryCategory> GetAllRecords()
        {
            return _inventoryCategoryDL.GetAllRecords();
        }
    }
}
