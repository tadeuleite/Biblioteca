using BibliotecaSenac.Business.InterfaceBusiness;
using BibliotecaSenac.Model;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaSenac.Controller
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReservaController : GenericController<ReservaModel>
    {
        private readonly IReservaBusiness reservaBusiness;

        public ReservaController(IReservaBusiness _reservaBusiness) : base(_reservaBusiness)
        {
            reservaBusiness = _reservaBusiness;
        }

    }
}