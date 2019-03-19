using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Charts
{
    interface IChartsRepositorio
    {
        int GetChartLine(Charts charts);
        int GetChartRound(Charts charts);
    }
}