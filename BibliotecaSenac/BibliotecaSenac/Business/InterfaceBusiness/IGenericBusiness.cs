using BibliotecaSenac.Model;
using System.Collections.Generic;

namespace BibliotecaSenac.Business
{
    public interface IGenericBusiness<T>
    {
        RetornoTratado<T> InserirValidar(T aluno, RetornoTratado<T> retorno);
        RetornoTratado<T> AlterarValidar(T aluno, RetornoTratado<T> retorno, string parametroAlterar);
        RetornoTratado<T> DeletarValidar(T aluno, RetornoTratado<T> retorno, string parametroDeletar);
        List<T> ConsultarValidar(T aluno, RetornoTratado<T> retorno);
        List<T> ConsultarComParametro(T aluno, RetornoTratado<T> retorno);
    }
}
