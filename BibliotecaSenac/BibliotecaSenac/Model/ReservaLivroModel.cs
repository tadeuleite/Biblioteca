using System.Collections.Generic;

namespace BibliotecaSenac.Model
{
    public class ReservaLivroModel
    {
        public int IdReservaLivro { get; set; }
        public ReservaModel ReservaModel { get; set; }
        public List<LivroModel> Livro { get; set; }

        public string NOMETABELA
        {
            get { return "RESERVALIVRO"; }
            private set { }
        }

        public ReservaLivroModel()
        {
            IdReservaLivro = -1;
            ReservaModel = new ReservaModel();
            Livro = new List<LivroModel>();
        }
    }
}
