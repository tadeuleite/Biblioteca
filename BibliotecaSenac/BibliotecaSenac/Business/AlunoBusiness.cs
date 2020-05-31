using BibliotecaSenac.Business.InterfaceBusiness;
using BibliotecaSenac.Model;
using BibliotecaSenac.Repository.InterfaceRepository;
using System;

namespace BibliotecaSenac.Business
{
    public class AlunoBusiness : IAlunoBusiness
    {
        private readonly IAlunoRepository alunoRepository;

        public AlunoBusiness()
        {

        }

        public RetornoTratado<AlunoModel> InserirValidar(AlunoModel aluno, RetornoTratado<AlunoModel> retorno)
        {
            if (aluno.Cpf == string.Empty)
            {

            }
            else
            {
                retorno = alunoRepository.InserirValidar(aluno);
            }
            
            return retorno;
        }
        public RetornoTratado<AlunoModel> AlterarValidar(AlunoModel aluno, RetornoTratado<AlunoModel> retorno)
        {
            throw new System.NotImplementedException();
        }

        public RetornoTratado<AlunoModel> DeletarValidar(AlunoModel aluno, RetornoTratado<AlunoModel> retorno)
        {
            throw new System.NotImplementedException();
        }

        public RetornoTratado<AlunoModel> ConsultarValidar(AlunoModel aluno)
        {
            throw new System.NotImplementedException();
        }

    }
}
