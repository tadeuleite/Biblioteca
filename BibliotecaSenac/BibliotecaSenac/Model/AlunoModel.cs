namespace BibliotecaSenac.Model
{
    public class AlunoModel
    {
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public string Email { get; set; }
        public string CodCartao { get; set; }
        public string Telefone { get; set; }
        public string Cpf { get; set; }

        public AlunoModel()
        {
            Nome = string.Empty;
            Matricula = string.Empty;
            Email = string.Empty;
            CodCartao = string.Empty;
            Telefone = string.Empty;
            Cpf = string.Empty;
        }
    }
}
