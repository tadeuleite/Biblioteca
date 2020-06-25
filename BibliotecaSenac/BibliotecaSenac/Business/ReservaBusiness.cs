using BibliotecaSenac.Business.InterfaceBusiness;
using BibliotecaSenac.Model;
using BibliotecaSenac.Repository.InterfaceRepository;

namespace BibliotecaSenac.Business
{
    public class ReservaBusiness : GenericBusiness<ReservaModel>, IReservaBusiness
    {
        private readonly IReservaRepository reservaRepository;

        public ReservaBusiness(IReservaRepository _reservaRepository) : base(_reservaRepository)
        {
            reservaRepository = _reservaRepository;
        }
    }
}
