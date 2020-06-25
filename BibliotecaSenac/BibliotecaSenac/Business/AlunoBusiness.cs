using BibliotecaSenac.Business.InterfaceBusiness;
using BibliotecaSenac.Model;
using BibliotecaSenac.Repository;
using BibliotecaSenac.Repository.InterfaceRepository;
using System.Collections.Generic;

namespace BibliotecaSenac.Business
{
    public class AlunoBusiness : GenericBusiness<AlunoModel>, IAlunoBusiness
    {
        private readonly IAlunoRepository alunoRepository;

        public AlunoBusiness(IAlunoRepository _alunoRepository) : base(_alunoRepository)
        {
            alunoRepository = _alunoRepository;
        }

        public List<RetornoEmprestimoAluno> ConsultarEmprestimosAluno(AlunoModel objeto, RetornoTratado<RetornoEmprestimoAluno> retorno)
        {
            var retornos = alunoRepository.ConsultarEmprestimosAluno(objeto, retorno);

            return retornos;
            
        }

        public List<RetornoEmprestimoAluno> ConsultarReservaAluno(AlunoModel objeto, RetornoTratado<RetornoEmprestimoAluno> retorno)
        {
            var retornos = alunoRepository.ConsultarReservaAluno(objeto, retorno);

            return retornos;
        }
    }
}
