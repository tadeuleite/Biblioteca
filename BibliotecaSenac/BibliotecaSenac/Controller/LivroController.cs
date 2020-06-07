using BibliotecaSenac.Business.InterfaceBusiness;
using BibliotecaSenac.Model;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaSenac.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LivroController : GenericController<LivroModel>
    {
        private readonly ILivroBusiness livroBusiness;

        public LivroController(ILivroBusiness _livroBusiness) : base(_livroBusiness)
        {
            livroBusiness = _livroBusiness;
        }

    }
}
