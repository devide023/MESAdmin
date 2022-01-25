using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesServices.Common;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.DB;
using ZDMesModels.TJ;
namespace MesAdmin.Controllers
{
    public class ValuesController : ApiController
    {
        private IDbOperate _service;
        private IDbConn _Con;
        public ValuesController(IDbOperate service,IDbConn conn)
        {
            _service = service;
            _Con = conn;
        }
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { _Con.DbConnStr, "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
