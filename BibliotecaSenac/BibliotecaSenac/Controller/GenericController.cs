using BibliotecaSenac.Business;
using BibliotecaSenac.Model;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaSenac.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class GenericController<T> : ControllerBase
    {
        private readonly IGenericBusiness<T> genericBusiness;
        
        public GenericController()
        {

        }

        public GenericController(IGenericBusiness<T> _genericBusiness)
        {
            genericBusiness = _genericBusiness;
        }

        [HttpPost]
        public IActionResult Inserir(T aluno)
        {
            RetornoTratado<T> retorno = new RetornoTratado<T>();

            retorno = genericBusiness.InserirValidar(aluno, retorno);

            return Ok(retorno);
        }

        [HttpPut]
        public IActionResult Alterar(T aluno, string parametroAlterar)
        {
            RetornoTratado<T> retorno = new RetornoTratado<T>();

            retorno = genericBusiness.AlterarValidar(aluno, retorno, parametroAlterar);

            return Ok(retorno);
        }

        [HttpDelete]
        public IActionResult Deletar(T aluno, string parametroDeletar)
        {
            RetornoTratado<T> retorno = new RetornoTratado<T>();

            retorno = genericBusiness.DeletarValidar(aluno, retorno, parametroDeletar);

            return Ok(retorno);
        }

        [HttpPost]
        public IActionResult Consultar([FromBody]T aluno)
        {
            RetornoTratado<T> retorno = new RetornoTratado<T>();
            var retornos = genericBusiness.ConsultarValidar(aluno, retorno);

            return Ok(retornos);
        }

        [HttpPost]
        public IActionResult ConsultarPorParametro([FromBody]T aluno, string parametroConsulta)
        {
            RetornoTratado<T> retorno = new RetornoTratado<T>();
            var retornos = genericBusiness.ConsultarValidar(aluno, retorno);

            return Ok(retornos);
        }
    }
}
