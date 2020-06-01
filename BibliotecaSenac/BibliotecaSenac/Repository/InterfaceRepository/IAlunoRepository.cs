using BibliotecaSenac.Model;

namespace BibliotecaSenac.Repository.InterfaceRepository
{
    public interface IAlunoRepository
    {
        RetornoTratado<AlunoModel> InserirValidar(AlunoModel aluno, RetornoTratado<AlunoModel> retorno);
        RetornoTratado<AlunoModel> AlterarValidar(AlunoModel aluno, RetornoTratado<AlunoModel> retorno);
        RetornoTratado<AlunoModel> DeletarValidar(AlunoModel aluno, RetornoTratado<AlunoModel> retorno);
        RetornoTratado<AlunoModel> ConsultarValidar(AlunoModel aluno, RetornoTratado<AlunoModel> retorno);
    }
}
