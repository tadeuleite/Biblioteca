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

        public RetornoTratado<T> InserirValidar(T aluno, RetornoTratado<T> retorno)
        {
            retorno = ValidarAluno(aluno, retorno);

            if (retorno.Erro == false)
            {
                retorno = repositoryGeneric.Inserir(aluno, retorno);
            }

            return retorno;
        }

        public RetornoTratado<T> AlterarValidar(T aluno, RetornoTratado<T> retorno, string parametroAlterar)
        {
            retorno = ValidarAluno(aluno, retorno);

            if (retorno.Erro == false)
            {
                retorno = repositoryGeneric.Alterar(aluno, retorno, parametroAlterar);
            }

            return retorno;
        }

        public RetornoTratado<T> DeletarValidar(T aluno, RetornoTratado<T> retorno, string parametroDeletar)
        {
            retorno = ValidarAluno(aluno, retorno);

            if (retorno.Erro == false)
            {
                retorno = repositoryGeneric.Deletar(aluno, retorno, parametroDeletar);
            }

            return retorno;
        }

        public List<T> ConsultarValidar(T aluno, RetornoTratado<T> retorno)
        {
            retorno = ValidarAluno(aluno, retorno);

            if (retorno.Erro == false)
            {
            }
            var retornos = repositoryGeneric.Consultar(aluno, retorno);
            return retornos;
        }

        public List<T> ConsultarPorParametro(T aluno, RetornoTratado<T> retorno, string parametroConsultar)
        {
            retorno = ValidarAluno(aluno, retorno);

            if (retorno.Erro == false)
            {
            }
            var retornos = repositoryGeneric.ConsultarPorParametro(aluno, retorno, parametroConsultar);
            return retornos;
        }


        private RetornoTratado<T> ValidarAluno(T aluno, RetornoTratado<T> retorno)
        {
            //// TODO: Validar se o cpf ta correto e se email ta no formato correto
            //if (aluno.Cpf == string.Empty)
            //{
            //    retorno.Erro = true;
            //    retorno.MensagemErro = "Cpf inválido";
            //}
            //else if (aluno.Matricula == string.Empty)
            //{
            //    retorno.Erro = true;
            //    retorno.MensagemErro = "Matricula inválida";
            //}
            //else if (aluno.Nome == string.Empty)
            //{
            //    retorno.Erro = true;
            //    retorno.MensagemErro = "Nome inválido";
            //}
            //else if (aluno.Email == string.Empty)
            //{
            //    retorno.Erro = true;
            //    retorno.MensagemErro = "E-mail inválido";
            //}

            return retorno;
        }
    }
}
