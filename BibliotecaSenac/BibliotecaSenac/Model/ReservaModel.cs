using System;
using System.Collections.Generic;

namespace BibliotecaSenac.Model
{
    public class ReservaModel 
    {
        public int IdReserva { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public AlunoModel Aluno { get; set; }
        public List<LivroModel> Livros { get; set; }

        public string NOMETABELA
        {
            get { return "RESERVA"; }
            private set { }
        }

        public ReservaModel() 
        {
            IdReserva = -1;
            Aluno = new AlunoModel();
            Livros = new List<LivroModel>();
        }
    }
}
