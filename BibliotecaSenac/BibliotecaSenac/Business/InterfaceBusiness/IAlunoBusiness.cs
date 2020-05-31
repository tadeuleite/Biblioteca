using BibliotecaSenac.Model;

namespace BibliotecaSenac.Business.InterfaceBusiness
{
    public interface IAlunoBusiness
    {
        RetornoTratado<AlunoModel> InserirValidar(AlunoModel aluno, RetornoTratado<AlunoModel>);
        RetornoTratado<AlunoModel> AlterarValidar(AlunoModel aluno, RetornoTratado<AlunoModel>);
        RetornoTratado<AlunoModel> DeletarValidar(AlunoModel aluno, RetornoTratado<AlunoModel>);
        RetornoTratado<AlunoModel> ConsultarValidar(AlunoModel aluno);
    }
}
