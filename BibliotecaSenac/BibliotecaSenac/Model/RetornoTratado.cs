namespace BibliotecaSenac.Model
{
    public class RetornoTratado<T>
    {
        public T Objeto { get; set; }
        public bool Erro { get; set; }
        public string MensagemErro { get; set; }

        public RetornoTratado()
        {
            Objeto = default;
            Erro = false;
            MensagemErro = string.Empty;
        }
    }
}
