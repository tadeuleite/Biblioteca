namespace BibliotecaSenac.Model
{
    public class LivroModel
    {
        public int IdLivro { get; set; }
        public string Nome { get; set; }
        public string Editora { get; set; }
        public string Versao { get; set; }
        public string Autor { get; set; }
        public int Quantidade { get; set; }

        public string NOMETABELA
        {
            get { return "Livro"; }
            private set { }
        }
        public LivroModel()
        {
            IdLivro = 0;
            Nome = string.Empty;
            Editora = string.Empty;
            Versao = string.Empty;
            Autor = string.Empty;
            Quantidade = 0;
        }
    }
}
