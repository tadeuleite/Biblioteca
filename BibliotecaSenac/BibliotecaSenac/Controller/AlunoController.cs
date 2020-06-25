using BibliotecaSenac.Business.InterfaceBusiness;
using BibliotecaSenac.Controller;
using BibliotecaSenac.Model;
using BibliotecaSenac.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaSenac.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlunoController : GenericController<AlunoModel>
    {
        private readonly IAlunoBusiness alunoBusiness;

        public AlunoController(IAlunoBusiness _alunoBusiness) : base(_alunoBusiness)
        {
            alunoBusiness = _alunoBusiness;
        }

        [HttpPost]
        public IActionResult ConsultarEmprestimosAluno([FromBody]AlunoModel objeto)
        {
            RetornoTratado<RetornoEmprestimoAluno> retorno = new RetornoTratado<RetornoEmprestimoAluno>();
            retorno.Objeto = alunoBusiness.ConsultarEmprestimosAluno(objeto, retorno);

            if (retorno.Erro == true)
                return Unauthorized(retorno);
            else
                return Ok(retorno);
        }

        [HttpPost]
        public IActionResult ConsultarReservaAluno([FromBody]AlunoModel objeto)
        {
            RetornoTratado<RetornoEmprestimoAluno> retorno = new RetornoTratado<RetornoEmprestimoAluno>();
            retorno.Objeto = alunoBusiness.ConsultarReservaAluno(objeto, retorno);

            if (retorno.Erro == true)
                return Unauthorized(retorno);
            else
                return Ok(retorno);
        }
    }
}
