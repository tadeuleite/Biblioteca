using System;

namespace BibliotecaSenac.Model
{
    public class ReservaModel
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public AlunoModel Aluno { get; set; }

        public ReservaModel()
        {
            Aluno = new AlunoModel();
        }
    }
}
