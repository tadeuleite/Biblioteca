using BibliotecaSenac.Model;
using System.Collections.Generic;

namespace BibliotecaSenac.Repository.InterfaceRepository
{
    public interface IGenericRepository<T>
    {
        RetornoTratado<T> Inserir(T aluno, RetornoTratado<T> retorno);
        RetornoTratado<T> Alterar(T aluno, RetornoTratado<T> retorno, string parametroAlterar);
        RetornoTratado<T> Deletar(T aluno, RetornoTratado<T> retorno, string parametroDeletar);
        List<T> Consultar(T aluno, RetornoTratado<T> retorno);
        List<T> ConsultarPorParametro(T aluno, RetornoTratado<T> retorno, string parametroConsultar);
    }
}
