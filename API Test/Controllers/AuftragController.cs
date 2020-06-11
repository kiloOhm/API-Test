using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;

namespace API_Test.Controllers
{
    public class AuftragController : ApiController
    {

        // GET: api/Auftrag/v1
        public List<Auftrag> Get(string id)
        {
            switch(id)
            {
                case "v1":
                    return Auftrag.getAuftrag(null);
                default:
                    return null;
            }
        }

        // POST: api/Auftrag/v1
        public void Post(string id, [FromBody] string value)
        {
            switch (id)
            {
                case "v1":
                    break;
                default:
                    break;
            }
        }

        // PUT: api/Auftrag//v1
        public void Put(string id, [FromBody] string value)
        {
            switch (id)
            {
                case "v1":
                    break;
                default:
                    break;
            }
        }

        // DELETE: api/Auftrag/v1
        public void Delete(string id, [FromBody] string value)
        {
            switch (id)
            {
                case "v1":
                    break;
                default:
                    break;
            }
        }
    }
}
