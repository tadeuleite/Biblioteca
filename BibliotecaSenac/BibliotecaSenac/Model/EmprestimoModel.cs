using System;
using System.Collections.Generic;

namespace BibliotecaSenac.Model
{
    public class EmprestimoModel
    {
        public int IdEmprestimo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public DateTime DataDevolucao { get; set; }
        public AlunoModel Aluno { get; set; }
        public List<LivroModel> Livros { get; set; }

        public string NOMETABELA
        {
            get { return "EMPRESTIMO"; }
            private set { }
        }
        public EmprestimoModel()
        {
            IdEmprestimo = -1;
            Aluno = new AlunoModel();
            Livros = new List<LivroModel>();
        }
    }
}
