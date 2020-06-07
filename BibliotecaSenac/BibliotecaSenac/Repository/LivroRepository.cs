using BibliotecaSenac.Model;
using BibliotecaSenac.Repository.InterfaceRepository;
using Microsoft.Extensions.Configuration;

namespace BibliotecaSenac.Repository
{
    public class LivroRepository : GenericRepository<LivroModel>, ILivroRepository
    {
        private readonly IConfiguration configuration;

        public LivroRepository(IConfiguration _configuration) : base(_configuration)
        {
            configuration = _configuration;
        }


    }
}
