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
    public class AgencyController : ApiController
    {
        // GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        public string Get(string id)
        {
            string json = "";
            DataTable dt = DbHelperACE.Query(string.Format(@"
SELECT   AgencyId as [id], AgencyName as [text],'open' as [state]
FROM      Agency
WHERE   (Status = '1') AND AreaId = {0}
ORDER BY Seq ASC
", id)).Tables[0];

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