using BibliotecaSenac.Model;
using BibliotecaSenac.Repository.InterfaceRepository;
using Microsoft.Extensions.Configuration;

namespace BibliotecaSenac.Repository
{
    public class AlunoRepository : GenericRepository<AlunoModel>, IAlunoRepository
    {
        private readonly IConfiguration configuration;

        public AlunoRepository(IConfiguration _configuration) : base(_configuration)
        {
            configuration = _configuration;
        }

      
    }
}
