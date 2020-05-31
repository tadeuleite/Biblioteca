namespace BibliotecaSenac.Model
{
    public class ReservaEmpresimoModel
    {
        public ReservaModel ReservaModel { get; set; }
        public EmprestimoModel EmprestimoModel { get; set; }

        public ReservaEmpresimoModel()
        {
            ReservaModel = new ReservaModel();
            EmprestimoModel = new EmprestimoModel();
        }
    }
}
