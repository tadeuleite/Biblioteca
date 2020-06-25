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

        /// <summary>
        /// Método genérico para inserir um dado no banco
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Inserir([FromBody]T objeto)
        {
            RetornoTratado<T> retorno = new RetornoTratado<T>();

            retorno = genericBusiness.InserirValidar(objeto, retorno);

            if (retorno.Erro == true)
                return Unauthorized(retorno);
            else
                return Ok(retorno);
        }

        /// <summary>
        /// Método genérico para alterar um dado, quando alterar, enviar todos os dados, os alterados e não alterados
        /// O parâmetro alterar é o nome da coluna do banco(mesma da model) que será usado como referência
        /// </summary>
        /// <param name="objeto"></param>
        /// <param name="parametroAlterar">Parâmetro identificador para alteração no banco</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Alterar([FromBody]T objeto, string parametroAlterar)
        {
            RetornoTratado<T> retorno = new RetornoTratado<T>();

            retorno = genericBusiness.AlterarValidar(objeto, retorno, parametroAlterar);

            if (retorno.Erro == true)
                return Unauthorized(retorno);
            else
                return Ok(retorno);
        }

        /// <summary>
        /// Método genérico para Deletar
        /// O parâmetro deletar é o nome da coluna do banco(mesma da model) que será usado como referência
        /// </summary>
        /// <param name="objeto"></param>
        /// <param name="parametroDeletar">Parâmetro identificador para alteração no banco</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Deletar([FromBody]T objeto, string parametroDeletar)
        {
            RetornoTratado<T> retorno = new RetornoTratado<T>();

            retorno = genericBusiness.DeletarValidar(objeto, retorno, parametroDeletar);

            if (retorno.Erro == true)
                return Unauthorized(retorno);
            else
                return Ok(retorno);
        }

        /// <summary>
        /// Método genérico para consultar todos registros
        /// </summary>
        /// <param name="objeto">Não necessário</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Consultar([FromBody]T objeto)
        {
            RetornoTratado<T> retorno = new RetornoTratado<T>();
            retorno.Objeto = genericBusiness.ConsultarValidar(objeto, retorno);

            if (retorno.Erro == true)
                return Unauthorized(retorno);
            else
                return Ok(retorno);
        }

        /// <summary>
        /// Método genérico para consultar um registro específico
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ConsultarComParametro([FromBody]T objeto)
        {
            RetornoTratado<T> retorno = new RetornoTratado<T>();
            retorno.Objeto = genericBusiness.ConsultarComParametro(objeto, retorno);

            if (retorno.Erro == true)
                return Unauthorized(retorno);
            else
                return Ok(retorno);
        }
    }
}
