using System;

namespace BibliotecaSenac.Model
{
    public class ReservaModel 
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public AlunoModel Aluno { get; set; }


        public string NOMETABELA
        {
            get { return "Reserva"; }
            private set { }
        }

        public ReservaModel() { }
        public ReservaModel(string nomeTabela) 
        {
            nomeTabela = NOMETABELA;
            Aluno = new AlunoModel();
        }
    }
}
