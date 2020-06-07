using BibliotecaSenac.Business.InterfaceBusiness;
using BibliotecaSenac.Model;
using BibliotecaSenac.Repository.InterfaceRepository;

namespace BibliotecaSenac.Business
{
    public class LivroBusiness : GenericBusiness<LivroModel>, ILivroBusiness
    {
        private readonly ILivroRepository livroRepository;

        public LivroBusiness(ILivroRepository _livroBusiness) : base(_livroBusiness)
        {
            livroRepository = _livroBusiness;
        }
    }
}
