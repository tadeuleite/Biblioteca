using BibliotecaSenac.Business.InterfaceBusiness;
using BibliotecaSenac.Model;
using BibliotecaSenac.Repository.InterfaceRepository;

namespace BibliotecaSenac.Business
{
    public class AlunoBusiness : GenericBusiness<AlunoModel>, IAlunoBusiness
    {
        private readonly IAlunoRepository alunoRepository;

        public AlunoBusiness(IAlunoRepository _alunoRepository) : base(_alunoRepository)
        {
            alunoRepository = _alunoRepository;
        }

    }
}
