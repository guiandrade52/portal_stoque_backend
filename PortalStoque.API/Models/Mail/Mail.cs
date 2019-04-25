using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalStoque.API.Models.Mail
{
    public class Mail
    {
        public string HTML { get; set; }
        public string From{ get; set; }
        public string Assunto { get; set; }

    }
}