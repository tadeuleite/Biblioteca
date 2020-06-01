using BibliotecaSenac.Business.InterfaceBusiness;
using BibliotecaSenac.Model;
using BibliotecaSenac.Repository.InterfaceRepository;

namespace BibliotecaSenac.Business
{
    public class AlunoBusiness : IAlunoBusiness
    {
        private readonly IAlunoRepository alunoRepository;

        public AlunoBusiness(IAlunoRepository _alunoRepository)
        {
            alunoRepository = _alunoRepository;
        }

        public RetornoTratado<AlunoModel> InserirValidar(AlunoModel aluno, RetornoTratado<AlunoModel> retorno)
        {
            retorno = ValidarAluno(aluno, retorno);

            if (retorno.Erro == false)
            {
                retorno = alunoRepository.InserirValidar(aluno, retorno);
            }

            return retorno;
        }

        public RetornoTratado<AlunoModel> AlterarValidar(AlunoModel aluno, RetornoTratado<AlunoModel> retorno)
        {
            retorno = ValidarAluno(aluno, retorno);

            if (retorno.Erro == false)
            {
                retorno = alunoRepository.AlterarValidar(aluno, retorno);
            }

            return retorno;
        }

        public RetornoTratado<AlunoModel> DeletarValidar(AlunoModel aluno, RetornoTratado<AlunoModel> retorno)
        {
            retorno = ValidarAluno(aluno, retorno);

            if (retorno.Erro == false)
            {
                retorno = alunoRepository.DeletarValidar(aluno, retorno);
            }

            return retorno;
        }

        public RetornoTratado<AlunoModel> ConsultarValidar(AlunoModel aluno, RetornoTratado<AlunoModel> retorno)
        {
            retorno = ValidarAluno(aluno, retorno);

            if (retorno.Erro == false)
            {
                retorno = alunoRepository.ConsultarValidar(aluno, retorno);
            }

            return retorno;
        }


        private RetornoTratado<AlunoModel> ValidarAluno(AlunoModel aluno, RetornoTratado<AlunoModel> retorno)
        {
            // TODO: Validar se o cpf ta correto e se email ta no formato correto
            if (aluno.Cpf == string.Empty)
            {
                retorno.Erro = true;
                retorno.MensagemErro = "Cpf inválido";
            }
            else if (aluno.Matricula == string.Empty)
            {
                retorno.Erro = true;
                retorno.MensagemErro = "Matricula inválida";
            }
            else if (aluno.Nome == string.Empty)
            {
                retorno.Erro = true;
                retorno.MensagemErro = "Nome inválido";
            }
            else if (aluno.Email == string.Empty)
            {
                retorno.Erro = true;
                retorno.MensagemErro = "E-mail inválido";
            }

            return retorno;
        }
    }
}
