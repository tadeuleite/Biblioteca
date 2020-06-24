using BibliotecaSenac.Model;
using BibliotecaSenac.Repository.InterfaceRepository;
using System.Collections.Generic;

namespace BibliotecaSenac.Business
{
    public abstract class GenericBusiness<T> : IGenericBusiness<T>
    {
        private readonly IGenericRepository<T> repositoryGeneric;

        public GenericBusiness()
        {

        }

        public GenericBusiness(IGenericRepository<T> _repositoryGeneric)
        {
            repositoryGeneric = _repositoryGeneric;
        }

        public RetornoTratado<T> InserirValidar(T objeto, RetornoTratado<T> retorno)
        {
            retorno = Validarobjeto(objeto, retorno);

            if (retorno.Erro == false)
            {
                retorno = repositoryGeneric.Inserir(objeto, retorno);
            }

            return retorno;
        }

        public RetornoTratado<T> AlterarValidar(T objeto, RetornoTratado<T> retorno, string parametroAlterar)
        {
            retorno = Validarobjeto(objeto, retorno);

            if (retorno.Erro == false)
            {
                retorno = repositoryGeneric.Alterar(objeto, retorno, parametroAlterar);
            }

            return retorno;
        }

        public RetornoTratado<T> DeletarValidar(T objeto, RetornoTratado<T> retorno, string parametroDeletar)
        {
            retorno = Validarobjeto(objeto, retorno);

            if (retorno.Erro == false)
            {
                retorno = repositoryGeneric.Deletar(objeto, retorno, parametroDeletar);
            }

            return retorno;
        }

        public List<T> ConsultarValidar(T objeto, RetornoTratado<T> retorno)
        {
            retorno = Validarobjeto(objeto, retorno);

            if (retorno.Erro == false)
            {
            }
            var retornos = repositoryGeneric.Consultar(objeto, retorno);
            return retornos;
        }

        public List<T> ConsultarComParametro(T objeto, RetornoTratado<T> retorno)
        {
            retorno = Validarobjeto(objeto, retorno);

            if (retorno.Erro == false)
            {
            }
            var retornos = repositoryGeneric.ConsultarComParametro(objeto, retorno);
            return retornos;
        }


        private RetornoTratado<T> Validarobjeto(T objeto, RetornoTratado<T> retorno)
        {
            //// TODO: Validar se o cpf ta correto e se email ta no formato correto
            //if (objeto.Cpf == string.Empty)
            //{
            //    retorno.Erro = true;
            //    retorno.MensagemErro = "Cpf inválido";
            //}
            //else if (objeto.Matricula == string.Empty)
            //{
            //    retorno.Erro = true;
            //    retorno.MensagemErro = "Matricula inválida";
            //}
            //else if (objeto.Nome == string.Empty)
            //{
            //    retorno.Erro = true;
            //    retorno.MensagemErro = "Nome inválido";
            //}
            //else if (objeto.Email == string.Empty)
            //{
            //    retorno.Erro = true;
            //    retorno.MensagemErro = "E-mail inválido";
            //}

            return retorno;
        }
    }
}
