using BibliotecaSenac.Business.InterfaceBusiness;
using BibliotecaSenac.Controller;
using BibliotecaSenac.Model;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaSenac.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlunoController : GenericController<AlunoModel>
    {
        private readonly IAlunoBusiness alunoBusiness;

        public AlunoController(IAlunoBusiness _alunoBusiness) : base(_alunoBusiness)
        {
            alunoBusiness = _alunoBusiness;
        }

    }
}
