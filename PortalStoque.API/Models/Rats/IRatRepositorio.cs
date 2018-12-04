using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Rats
{
    interface IRatRepositorio
    {
        IEnumerable<Rat> GetDataRat(int executionId);
        string GetRATXML(int executionId, int visita);
    }
}