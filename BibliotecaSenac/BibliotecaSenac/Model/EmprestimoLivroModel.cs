namespace BibliotecaSenac.Model
{
    public class EmprestimoLivroModel
    {
        public EmprestimoModel Emprestimo { get; set; }
        public LivroModel Livro { get; set; }

        public string NOMETABELA
        {
            get { return "EmprestimoLivro"; }
            private set { }
        }
        public EmprestimoLivroModel()
        {
            Emprestimo = new EmprestimoModel();
            Livro = new LivroModel();
        }
    }
}
