using System.Collections.Generic;

namespace BibliotecaSenac.Model
{
    public class RetornoTratado<T>
    {
        public List<T> Objeto { get; set; }
        public bool Erro { get; set; }
        public string MensagemErro { get; set; }
        public string ErroLocal { get; set; }

        public RetornoTratado()
        {
            Objeto = default;
            Erro = false;
            MensagemErro = string.Empty;
            ErroLocal = string.Empty;
        }
    }
}
