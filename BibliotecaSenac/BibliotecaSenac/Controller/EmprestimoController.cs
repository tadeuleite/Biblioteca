using BibliotecaSenac.Business.InterfaceBusiness;
using BibliotecaSenac.Model;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaSenac.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmprestimoController : GenericController<EmprestimoModel>
    {
        private readonly IEmprestimoBusiness emprestimoBusiness;

        public EmprestimoController(IEmprestimoBusiness _emprestimoBusiness) : base(_emprestimoBusiness)
        {
            emprestimoBusiness = _emprestimoBusiness;
        }

    }
}
