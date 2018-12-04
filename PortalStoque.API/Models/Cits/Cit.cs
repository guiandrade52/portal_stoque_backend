using System;

namespace PortalStoque.API.Models.Cits
{
    public class Cit
    {
        public int ExecutionId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFinal { get; set; }
    }
}