using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Models;

namespace API_Test.Controllers
{
    /// <summary>
    /// Rest API für Aufträge
    /// </summary>
    public class AuftragController : ApiController
    {
        /// <summary>
        /// Gibt eine List der Aufträge zurück, welche mit den parametern übereinstimmen.
        /// Ohne Parameter werden alle Aufträge zurückgegeben.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="auftrag"></param>
        /// <returns></returns>
        public List<Auftrag> Get(string version, [FromUri] Auftrag auftrag)
        {
            switch (version)
            {
                case "v1":
                    return Auftrag.getAuftrag(auftrag);
                default:
                    return null;
            }
        }

        /// <summary>
        /// Fügt einen neuen Auftrag hinzu.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="auftragBP"></param>
        public HttpResponseMessage Post(string version, [FromBody] AuftragBP auftragBP)
        {
            switch (version)
            {
                case "v1":
                    if (auftragBP == null) return new HttpResponseMessage {StatusCode = HttpStatusCode.BadRequest};
                    try
                    {
                        Auftrag auftrag = new Auftrag
                        {
                            ID = new Guid(),
                            Kunde = auftragBP.Kunde,
                            Position = auftragBP.Position,
                            Volumen = auftragBP.Volumen,
                            Deadline = auftragBP.Deadline
                        };

                        string failure = Auftrag.addAuftrag(auftrag);
                        if (failure != "") return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = failure };
                        else return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
                    }
                    catch (Exception e)
                    {
                        return new HttpResponseMessage
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            ReasonPhrase = e.Message
                        };
                    }
                default:
                    return new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound };
            }
        }

        /// <summary>
        /// Ändert Daten innerhalb eines Auftrags
        /// </summary>
        /// <param name="version"></param>
        /// <param name="auftrag"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(string version, [FromBody] Auftrag auftrag)
        {
            switch (version)
            {
                case "v1":
                    try
                    {
                        string failure = Auftrag.updateAuftrag(auftrag);
                        if (failure != "") return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = failure };
                        else return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
                    }
                    catch(Exception e)
                    {
                        return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = e.Message };
                    }
                default:
                    return new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound };
            }
        }

        /// <summary>
        /// Löscht einen Auftrag
        /// </summary>
        /// <param name="version"></param>
        /// <param name="auftrag"></param>
        /// <returns></returns>
        public HttpResponseMessage Delete(string version, [FromBody] Auftrag auftrag)
        {
            switch (version)
            {
                case "v1":
                    try
                    {
                        string failure = Auftrag.deleteAuftrag(auftrag);
                        if (failure != "") return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = failure };
                        else return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
                    }
                    catch (Exception e)
                    {
                        return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest, ReasonPhrase = e.Message };
                    }
                default:
                    return new HttpResponseMessage { StatusCode = HttpStatusCode.NotFound };
            }
        }
    }
}
