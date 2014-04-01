using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Newtonsoft.Json;

namespace UttInventory.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        public ActionResult Area(string AreaId,string Mode)
        {
            DataTable dt;
            
            ViewBag.Mode = Mode.Trim().ToUpper();

            switch (Mode.Trim().ToUpper())
            {
                case "ADD":
                    ViewBag.ModeName = "新增";
                    dt = DbHelperACE.Query(string.Format(@"
SELECT  '0' as AreaId,'' as AreaName,'1' as Status,'1' as Seq
")).Tables[0];
                    break;
                case "EDIT":
                    ViewBag.ModeName = "编辑";
                    dt = DbHelperACE.Query(string.Format(@"
SELECT  AreaId, AreaName,Status,Seq
FROM   Area
WHERE AreaId = {0}
", AreaId)).Tables[0];
                    break;
                default:
                    throw new Exception("状态错误");
            }

            if (dt.Rows.Count <= 0)
            {
                throw new Exception("区域不存在");
            }

            ViewBag.Area = dt.Rows[0];

            return View();
        }

        [HttpPost]
        public ActionResult Area(string AreaId, string Mode,string Seq,string Status,string AreaName)
        {
            string SQL = "";

            try
            {
                switch (Mode.Trim().ToUpper())
                {
                    case "ADD":
                        SQL = string.Format(@" INSERT INTO AREA (AreaName,Seq,Status) VALUES('{0}',{1},'{2}')", AreaName, Seq, Status);
                        break;
                    case "EDIT":
                        SQL = string.Format(@"
UPDATE Area
Set AREANAME = '{0}',Seq = {1},Status = '{2}'
WHERE AreaId = {3}
", AreaName, Seq, Status, AreaId);
                        break;
                    default:
                        throw new Exception("状态错误");
                }

                DbHelperACE.ExecuteSql(SQL);

                return Content("<script>parent.parent.AreaReload();</script>");
            }
            catch(Exception ex)
            {
                return Content("<script>alert(" + ex.Message + ");</script>");
            }
        }

        public ActionResult Agency(string AgencyId, string Mode,string AreaId)
        {
            DataTable dt;

            ViewBag.Mode = Mode.Trim().ToUpper();

            switch (Mode.Trim().ToUpper())
            {
                case "ADD":
                    ViewBag.ModeName = "新增";
                    dt = DbHelperACE.Query(string.Format(@"
SELECT  '0' as AgencyId,'' as AgencyName,'1' as Status,'1' as Seq,{0} as AreaId
",AreaId)).Tables[0];
                    break;
                case "EDIT":
                    ViewBag.ModeName = "编辑";
                    dt = DbHelperACE.Query(string.Format(@"
SELECT  AgencyId,AgencyName,Status,Seq,AreaId
FROM   Agency
WHERE AgencyId = {0}
", AgencyId)).Tables[0];
                    break;
                default:
                    throw new Exception("状态错误");
            }

            if (dt.Rows.Count <= 0)
            {
                throw new Exception("经销商不存在");
            }

            ViewBag.Agency = dt.Rows[0];

            return View();
        }


        [HttpPost]
        public ActionResult Agency(string AgencyId, string Mode, string Seq, string Status, string AgencyName,string AreaId)
        {
            string SQL = "";

            try
            {
                switch (Mode.Trim().ToUpper())
                {
                    case "ADD":
                        SQL = string.Format(@" INSERT INTO Agency (AgencyName,Seq,Status,AreaId) VALUES('{0}',{1},'{2}',{3})", AgencyName, Seq, Status,AreaId);
                        break;
                    case "EDIT":
                        SQL = string.Format(@"
UPDATE Agency
Set AgencyNAME = '{0}',Seq = {1},Status = '{2}'
WHERE AgencyId = {3}
", AgencyName, Seq, Status, AgencyId);
                        break;
                    default:
                        throw new Exception("状态错误");
                }

                DbHelperACE.ExecuteSql(SQL);

                return Content("<script>parent.parent.AgencyReload();</script>");
            }
            catch (Exception ex)
            {
                return Content("<script>alert(" + ex.Message + ");</script>");
            }
        }


        public ActionResult Product(string ProductId, string ProductType, string Mode, string AgencyId)
        {
            DataTable dt;

            ViewBag.Mode = Mode.Trim().ToUpper();

            switch (Mode.Trim().ToUpper())
            {
                case "ADD":
                    ViewBag.ModeName = "新增";
                    dt = DbHelperACE.Query(string.Format(@"
SELECT  '0' as ProductId,'' as ProductName,'1' as Status,'1' as Seq,{0} as AgencyId,'0' as QtyAvailable,'' as ProductType
", AgencyId)).Tables[0];
                    break;
                case "EDIT":
                    ViewBag.ModeName = "编辑";
                    dt = DbHelperACE.Query(string.Format(@"
SELECT   ProductId, ProductName, Status, Seq, CrtBy, CrtDate, AreaId, AgencyId, QtyAvailable,ProductType
FROM      Product
WHERE   ProductId = {0}
", ProductId)).Tables[0];
                    break;
                default:
                    throw new Exception("状态错误");
            }

            if (dt.Rows.Count <= 0)
            {
                throw new Exception("产品不存在");
            }

            ViewBag.Product = dt.Rows[0];

            return View();
        }



        [HttpPost]
        public ActionResult Product(string ProductId,string ProductType, string Mode, string Status, string ProductName, string AgencyId)
        {
            string SQL = "";

            try
            {
                switch (Mode.Trim().ToUpper())
                {
                    case "ADD":
                        SQL = string.Format(@"
INSERT INTO Product (ProductName,Status,AgencyId,QtyAvailable,QtyAvailableUpdatedDate,ProductType) VALUES('{0}','{1}',{2},0,now(),'{3}')", ProductName, Status, AgencyId, ProductType);
                        break;
                    case "EDIT":
                        SQL = string.Format(@"
UPDATE Product
Set ProductNAME = '{0}',Status = '{1}',ProductType = '{3}'
WHERE ProductId = {2}
", ProductName, Status, ProductId, ProductType);
                        break;
                    default:
                        throw new Exception("状态错误");
                }

                DbHelperACE.ExecuteSql(SQL);

                return Content("<script>parent.parent.ProductReload('winProduct');</script>");
            }
            catch (Exception ex)
            {
                return Content("<script>alert(" + ex.Message + ");</script>");
            }
        }
    }
}
