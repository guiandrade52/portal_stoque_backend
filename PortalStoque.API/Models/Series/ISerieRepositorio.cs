using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Series
{
    interface ISerieRepositorio
    {
        IEnumerable<Serie> GetAll(string filter);
        SerieDetails GetSerieDetails(string serie);
        IEnumerable<SerieParcCon> GetAllParcCon(string serie);
    }
}