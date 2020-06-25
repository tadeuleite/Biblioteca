using System.Collections.Generic;

namespace BibliotecaSenac.Model
{
    public class EmprestimoLivroModel
    {
        public int IdEmprestimoLivro { get; set; }
        public EmprestimoModel Emprestimo { get; set; }
        public List<LivroModel> Livros { get; set; }

        public string NOMETABELA
        {
            get { return "EMPRESTIMOLIVRO"; }
            private set { }
        }
        public EmprestimoLivroModel()
        {
            IdEmprestimoLivro = -1;
            Emprestimo = new EmprestimoModel();
            Livros = new List<LivroModel>();
        }
    }
}
