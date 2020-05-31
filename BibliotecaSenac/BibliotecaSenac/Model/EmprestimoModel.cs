using System;

namespace BibliotecaSenac.Model
{
    public class EmprestimoModel
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public DateTime DatDevolucao { get; set; }
        public AlunoModel Aluno { get; set; }

        public EmprestimoModel()
        {
            Aluno = new AlunoModel();
        }
    }
}
