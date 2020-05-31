using BibliotecaSenac.Model;

namespace BibliotecaSenac.Repository.InterfaceRepository
{
    public interface IAlunoRepository
    {
        bool InserirValidar(AlunoModel aluno);
        bool AlterarValidar(AlunoModel aluno);
        bool DeletarValidar(AlunoModel aluno);
        RetornoTratado<AlunoModel> ConsultarValidar(AlunoModel aluno);
    }
}
