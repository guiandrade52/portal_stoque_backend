using PortalStoque.API.Models.Contratos;
using PortalStoque.API.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class UsuarioPortalController : ApiController
    {
        static readonly IUsuarioRepositorio _usuarioRepositorio = new UsuarioRepositorio();
        static readonly IContratoRepositorio _contratoRepositorio = new ContratoRepositorio();

        public HttpResponseMessage GetAll(string search)
        {
            var u = new services.UsuarioCorrent();
            var permisoes = u.GetPermisoes();
            string filter = QueryUsuario.GetFilter(permisoes, search);

            return Request.CreateResponse(HttpStatusCode.OK, _usuarioRepositorio.GetAll(filter));
        }

        [HttpGet]
        public HttpResponseMessage CloneConfigUsuario(int usuarioBase, string usuariosReceptores, bool configuracao)
        {
            List<ParcConConfigs> configs = (List<ParcConConfigs>)_usuarioRepositorio.GetParcCon(usuarioBase);
            var receptores = usuariosReceptores.Split(',');


            for (int i = 0; i < receptores.Length; i++)
            {
                if (!configuracao)
                    _contratoRepositorio.DeleteAllContrato(Convert.ToInt32(receptores[i]));
                for (int r = 0; r < configs.Count(); r++)
                {
                    _contratoRepositorio.SalvarContrato(Convert.ToInt32(receptores[i]), configs[r].CodParcAb, configs[r].Contrato, configs[r].CodParcAt);
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }


    }
}
