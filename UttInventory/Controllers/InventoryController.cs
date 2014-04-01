using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;

namespace UttInventory.Controllers
{
    public class InventoryController : Controller
    {
        //
        // GET: /Inventory/

        public ActionResult Adjust(string ProductId, string Mode)
        {
            DataTable dt = DbHelperACE.Query(string.Format(@"
SELECT   ProductId, ProductName, Status, Seq, CrtBy, CrtDate, AreaId, AgencyId, QtyAvailable, QtyAvailableUpdatedDate
FROM      Product
WHERE   ProductId = {0}
ORDER BY Seq ASC
", ProductId)).Tables[0];

            if (dt.Rows.Count <= 0)
            {
                throw new Exception("产品不存在");
            }

            ViewBag.Mode = Mode.Trim().ToUpper();

            switch (Mode.Trim().ToUpper())
            {
                case "ADD":
                    ViewBag.ModeName = "加";
                    break;
                case "SUBTRACT":
                    ViewBag.ModeName = "减";
                    break;
                default:
                    throw new Exception("状态错误");
            }

            ViewBag.Product = dt.Rows[0];

            return View();
        }

        [HttpPost]
        public ActionResult Adjust(string ProductId, string Mode, string QtyAvailableAdjust)
        {
           int rows = DbHelperACE.ExecuteSql(string.Format(@"
Update Product Set QtyAvailable = IIF(IsNULL(QtyAvailable),0,QtyAvailable) + {0} , QtyAvailableUpdatedDate = now()
Where ProductId = {1}
", QtyAvailableAdjust, ProductId));
           return Content("<script>parent.parent.ProductReload('winAdjustProduct');</script>");
        }
    }
}
