using BibliotecaSenac.Model;
using System.Collections.Generic;

namespace BibliotecaSenac.Repository.InterfaceRepository
{
    public interface IAlunoRepository : IGenericRepository<AlunoModel>
    {
        List<RetornoEmprestimoAluno> ConsultarEmprestimosAluno(AlunoModel objeto, RetornoTratado<RetornoEmprestimoAluno> retorno);
        List<RetornoEmprestimoAluno> ConsultarReservaAluno(AlunoModel objeto, RetornoTratado<RetornoEmprestimoAluno> retorno);
    }
}
