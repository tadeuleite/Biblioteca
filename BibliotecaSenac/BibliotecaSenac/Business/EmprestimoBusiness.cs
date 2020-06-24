using BibliotecaSenac.Business.InterfaceBusiness;
using BibliotecaSenac.Model;
using BibliotecaSenac.Repository.InterfaceRepository;

namespace BibliotecaSenac.Business
{
    public class EmprestimoBusiness : GenericBusiness<EmprestimoModel>, IEmprestimoBusiness
    {
        private readonly IEmprestimoRepository emprestimoRepository;

        public EmprestimoBusiness(IEmprestimoRepository _emprestimoRepository) : base(_emprestimoRepository)
        {
            emprestimoRepository = _emprestimoRepository;
        }
    }
}
