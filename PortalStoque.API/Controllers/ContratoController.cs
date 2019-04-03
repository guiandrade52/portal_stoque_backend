using PortalStoque.API.Models.Contratos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PortalStoque.API.Controllers
{
    [Authorize]
    public class ContratoController : ApiController
    {
        static readonly IContratoRepositorio _contratoRepositorio = new ContratoRepositorio();
        public HttpResponseMessage GetAll(string search)
        {
            var u = new services.UsuarioCorrent();
            var user = u.GetPermisoes();
            string filter = QueryContrato.GetFilter(user, search);

            return Request.CreateResponse(HttpStatusCode.OK, _contratoRepositorio.GetAll(filter));
        }

        public HttpResponseMessage GetEndereco(int contrato)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _contratoRepositorio.GetDetails(contrato));
        }

        [HttpGet]
        public HttpResponseMessage ContratoPParceiro(string parceiros)
        {
            var contratos = _contratoRepositorio.GetAll(QueryContrato.ContratoPParceiro(parceiros));
            return Request.CreateResponse(HttpStatusCode.OK, contratos);
        }

        public HttpResponseMessage GetContratosUsuario(int idUsuario)
        {
            return Request.CreateResponse(HttpStatusCode.OK, _contratoRepositorio.ListaContratosPUsuario(idUsuario));
        }

        [HttpGet]
        public HttpResponseMessage DeletarContrato(int idUsuario, int contrato, int codParc)
        {
            _contratoRepositorio.DeleteContrato(idUsuario, contrato, codParc);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet]
        public HttpResponseMessage CadastroDeContrato(int idUsuario, int contrato, int codParc, string codParcAt)
        {
            var array = codParcAt.Split(',');
           ArrayList exists = new ArrayList(10);

            for (int i = 0; i < array.Length; i++)
                if (_contratoRepositorio.SalvarContrato(idUsuario, codParc, contrato, Convert.ToInt32(array[i])) != -1)
                    exists.Add(new { contrato, parceiro = array[i] });

            if(exists.Count > 0)
                return Request.CreateResponse(HttpStatusCode.PartialContent, new { Message = "Alguns contratos não foram cadastrados "+ exists.ToString(), contratos = exists});

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
