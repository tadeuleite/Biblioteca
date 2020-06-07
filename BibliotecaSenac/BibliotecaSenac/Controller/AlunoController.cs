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

        public AlunoController(IAlunoBusiness _alunoBusiness)
        {
            alunoBusiness = _alunoBusiness;
        }

        [HttpPost]
        public IActionResult Inserir(AlunoModel aluno)
        {
            RetornoTratado<AlunoModel> retorno = new RetornoTratado<AlunoModel>();

            retorno = alunoBusiness.InserirValidar(aluno, retorno);
            
            return Ok(retorno);
        }

        [HttpPut]
        public IActionResult Alterar(AlunoModel aluno, string parametroAlterar)
        {
            RetornoTratado<AlunoModel> retorno = new RetornoTratado<AlunoModel>();

            retorno = alunoBusiness.AlterarValidar(aluno, retorno, parametroAlterar);

            return Ok(retorno);
        }

        [HttpDelete]
        public IActionResult Deletar(AlunoModel aluno, string parametroDeletar)
        {
            RetornoTratado<AlunoModel> retorno = new RetornoTratado<AlunoModel>();

            retorno = alunoBusiness.DeletarValidar(aluno, retorno, parametroDeletar);

            return Ok(retorno);
        }

        [HttpPost]
        public IActionResult Consultar([FromBody]AlunoModel aluno)
        {
            RetornoTratado<AlunoModel> retorno = new RetornoTratado<AlunoModel>();
            retorno.Objeto = new AlunoModel();
            var retornos = alunoBusiness.ConsultarValidar(aluno, retorno);

            return Ok(retornos);
        }
        
        [HttpPost]
        public IActionResult ConsultarPorParametro([FromBody]AlunoModel aluno, string parametroConsulta)
        {
            RetornoTratado<AlunoModel> retorno = new RetornoTratado<AlunoModel>();
            retorno.Objeto = new AlunoModel();
            var retornos = alunoBusiness.ConsultarValidar(aluno, retorno);

            return Ok(retornos);
        }
    }
}
