using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Newtonsoft.Json;

namespace UttInventory.Services
{
    public class ProductController : ApiController
    {
        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        public string GetList(string id)
        {
            string json = "";
            DataTable dt = DbHelperACE.Query(string.Format(@"
SELECT   P.ProductId, P.ProductName, P.Status, P.Seq, P.CrtBy, P.CrtDate, P.AreaId, P.AgencyId, P.QtyAvailable, P.QtyAvailableUpdatedDate,T.TypeName as ProductTypeName
FROM      Product P
LEFT JOIN ProductType T ON P.ProductType = T.TypeId
WHERE   (P.Status = '1') AND P.AgencyId = {0}
ORDER BY P.Seq ASC
", id)).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
                timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                json = JsonConvert.SerializeObject(dt, timeConverter);
            }
            return json;
        }

        [HttpGet]
        [ActionName("GetTypeList")]
        public string GetList()
        {
            string json = "";
            DataTable dt = DbHelperACE.Query(@"
SELECT  TypeId as [id], TypeName as [text]
FROM   ProductType
WHERE   (Status = '1')
ORDER BY Seq ASC
").Tables[0];

            if (dt != null && dt.Rows.Count > 0)
            {
                Newtonsoft.Json.Converters.IsoDateTimeConverter timeConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter();
                timeConverter.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

                json = JsonConvert.SerializeObject(dt, timeConverter);
            }
            return json;
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}