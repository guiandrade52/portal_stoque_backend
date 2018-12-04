using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Cits
{
    interface ICitRepositorio
    {
        Cit GetCit(int id);
    }
}