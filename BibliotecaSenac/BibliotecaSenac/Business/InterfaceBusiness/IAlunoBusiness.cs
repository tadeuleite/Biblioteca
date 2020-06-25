using BibliotecaSenac.Model;
using BibliotecaSenac.Repository;
using System.Collections.Generic;

namespace BibliotecaSenac.Business.InterfaceBusiness
{
    public interface IAlunoBusiness : IGenericBusiness<AlunoModel>
    {
        List<RetornoEmprestimoAluno> ConsultarEmprestimosAluno(AlunoModel objeto, RetornoTratado<RetornoEmprestimoAluno> retorno);
        List<RetornoEmprestimoAluno> ConsultarReservaAluno(AlunoModel objeto, RetornoTratado<RetornoEmprestimoAluno> retorno);
    }
}
