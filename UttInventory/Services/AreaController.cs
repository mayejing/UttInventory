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
    public class AreaController : ApiController
    {
        // GET api/<controller>
        public string Get()
        {
            string json = "";
            DataTable dt = DbHelperACE.Query(@"
SELECT  AreaId as [id], AreaName as [text],'open' as [state]
FROM   Area
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

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
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