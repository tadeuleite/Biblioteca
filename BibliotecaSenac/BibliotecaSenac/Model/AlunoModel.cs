namespace BibliotecaSenac.Model
{
    public class AlunoModel
    {
        public int IdAluno { get; set; }
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public string Email { get; set; }
        public string CodCartao { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }

        public string NOMETABELA
        {
            get { return "ALUNO"; }
            private set { }
        }
        // Preencher NomeTabela com o nome da tabela no banco, propriedades com mesmo nome do banco e id começando com -1
        public AlunoModel()
        {
            IdAluno = -1;
            Nome = string.Empty;
            Matricula = string.Empty;
            Email = string.Empty;
            CodCartao = string.Empty;
            Telefone = string.Empty;
            Cpf = string.Empty;
        }
    }
}
