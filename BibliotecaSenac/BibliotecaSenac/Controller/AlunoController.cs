using BibliotecaSenac.Business.InterfaceBusiness;
using BibliotecaSenac.Model;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaSenac.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IAlunoBusiness alunoBusiness;

        public AlunoController()
        {
        }

        [HttpPost]
        public IActionResult Inserir(AlunoModel aluno)
        {
            RetornoTratado<AlunoModel> retorno = new RetornoTratado<AlunoModel>();

            retorno.Erro = alunoBusiness.InserirValidar(aluno, retorno);
            
            return Ok(retorno);
        }

        [HttpPut]
        public IActionResult Alterar(AlunoModel aluno)
        {
            RetornoTratado<AlunoModel> retorno = new RetornoTratado<AlunoModel>();

            retorno.Erro = alunoBusiness.AlterarValidar(aluno, retorno);

            return Ok(retorno);
        }

        [HttpDelete]
        public IActionResult Deletar(AlunoModel aluno)
        {
            RetornoTratado<AlunoModel> retorno = new RetornoTratado<AlunoModel>();

            retorno.Erro = alunoBusiness.DeletarValidar(aluno, retorno);

            return Ok(retorno);
        }

        [HttpGet]
        public IActionResult Consultar(AlunoModel aluno)
        {
            RetornoTratado<AlunoModel> retorno = alunoBusiness.ConsultarValidar(aluno);

            return Ok(retorno);
        }
    }
}
