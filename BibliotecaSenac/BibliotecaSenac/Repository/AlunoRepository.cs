using BibliotecaSenac.Model;
using BibliotecaSenac.Repository.InterfaceRepository;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BibliotecaSenac.Repository
{
    public class AlunoRepository : GenericRepository<AlunoModel>, IAlunoRepository
    {
        private readonly IConfiguration configuration;

        public AlunoRepository(IConfiguration _configuration) : base(_configuration)
        {
            configuration = _configuration;
        }

        public List<RetornoEmprestimoAluno> ConsultarReservaAluno(AlunoModel objeto, RetornoTratado<RetornoEmprestimoAluno> retorno)
        {
            var retornoEmprestimoAlunosLista = new List<RetornoEmprestimoAluno>();
            var retornoEmprestimoAlunos = new RetornoEmprestimoAluno();

            using (var conexao = new SqlConnection(configuration.GetValue<string>("SqlConnection")))
            {
                try
                {
                    IDbCommand comando = conexao.CreateCommand();

                    var camposObjetoGenerico = ObterNomePropriedades(objeto);

                    StringBuilder camposPesquisa = new StringBuilder();
                    if (camposObjetoGenerico.Contains("NOMETABELA"))
                        camposObjetoGenerico.Remove("NOMETABELA");

                    foreach (var item in camposObjetoGenerico)
                    {
                        camposPesquisa = item == camposObjetoGenerico.Last() ? camposPesquisa.Append(item) : camposPesquisa.Append(item + ", ");
                    }

                    comando.CommandText =
                        $@"SELECT 
                           LIVRO.NOME AS NOME_LIVRO, ALUNO.NOME AS NOME_ALUNO, DATAFIM
                            FROM ALUNO AS ALUNO
                           INNER JOIN RESERVA AS RESERVA
                            ON ALUNO.IDALUNO = RESERVA.IDALUNO
                           INNER JOIN RESERVALIVRO AS RESERVALIVRO
                            ON RESERVALIVRO.IDRESERVA = RESERVA.IDRESERVA
                           INNER JOIN LIVRO AS LIVRO
                            ON LIVRO.IDLIVRO = RESERVALIVRO.IDLIVRO
                            ";

                    if (conexao.State == ConnectionState.Closed)
                        conexao.Open();
                    var linhasRetornadas = comando.ExecuteReader();



                    while (linhasRetornadas.Read())
                    {
                        retornoEmprestimoAlunos.NomeAluno = linhasRetornadas["NOME_ALUNO"].ToString();
                        retornoEmprestimoAlunos.NomeLivro = linhasRetornadas["NOME_LIVRO"].ToString();
                        retornoEmprestimoAlunos.DataFim = linhasRetornadas["DATAFIM"].ToString();

                        retornoEmprestimoAlunosLista.Add(retornoEmprestimoAlunos);
                    }

                    linhasRetornadas.Close();

                    conexao.Close();

                }
                catch (Exception exception)
                {
                    conexao.Close();

                    retorno.Erro = true;
                    retorno.ErroLocal = exception.StackTrace;
                    retorno.MensagemErro = "Erro ao consultar no banco";
                }
                finally
                {
                    if (conexao.State == ConnectionState.Open)
                    {
                        conexao.Close();
                    }
                }
                return retornoEmprestimoAlunosLista;
            }
        }
        public List<RetornoEmprestimoAluno> ConsultarEmprestimosAluno(AlunoModel objeto, RetornoTratado<RetornoEmprestimoAluno> retorno)
        {
            var retornoEmprestimoAlunosLista = new List<RetornoEmprestimoAluno>();
            var retornoEmprestimoAlunos = new RetornoEmprestimoAluno();

            using (var conexao = new SqlConnection(configuration.GetValue<string>("SqlConnection")))
            {
                try
                {
                    IDbCommand comando = conexao.CreateCommand();

                    var camposObjetoGenerico = ObterNomePropriedades(objeto);

                    StringBuilder camposPesquisa = new StringBuilder();
                    if (camposObjetoGenerico.Contains("NOMETABELA"))
                        camposObjetoGenerico.Remove("NOMETABELA");

                    foreach (var item in camposObjetoGenerico)
                    {
                        camposPesquisa = item == camposObjetoGenerico.Last() ? camposPesquisa.Append(item) : camposPesquisa.Append(item + ", ");
                    }

                    comando.CommandText =
                        $@"SELECT 
                                LIVRO.NOME AS NOME_LIVRO, ALUNO.NOME AS NOME_ALUNO, DATADEVOLUCAO, DATAFIM
                            FROM ALUNO AS ALUNO
                            INNER JOIN EMPRESTIMO AS EMPRESTIMO
                                ON ALUNO.IDALUNO = EMPRESTIMO.IDALUNO
                            INNER JOIN EMPRESTIMOLIVRO AS EMPRESTIMOLIVRO
                                ON EMPRESTIMOLIVRO.IDEMPRESTIMO = EMPRESTIMO.IDEMPRESTIMO
                            INNER JOIN LIVRO AS LIVRO
                                ON LIVRO.IDLIVRO = EMPRESTIMOLIVRO.IDLIVRO
                            ";

                    if (conexao.State == ConnectionState.Closed)
                        conexao.Open();
                    var linhasRetornadas = comando.ExecuteReader();



                    while (linhasRetornadas.Read())
                    {
                        retornoEmprestimoAlunos.NomeAluno = linhasRetornadas["NOME_ALUNO"].ToString();
                        retornoEmprestimoAlunos.NomeLivro = linhasRetornadas["NOME_LIVRO"].ToString();
                        retornoEmprestimoAlunos.DataDevolucao = linhasRetornadas["DATADEVOLUCAO"].ToString();
                        retornoEmprestimoAlunos.DataFim = linhasRetornadas["DATAFIM"].ToString();

                        retornoEmprestimoAlunosLista.Add(retornoEmprestimoAlunos);
                    }

                    linhasRetornadas.Close();

                    conexao.Close();

                }
                catch (Exception exception)
                {
                    conexao.Close();

                    retorno.Erro = true;
                    retorno.ErroLocal = exception.StackTrace;
                    retorno.MensagemErro = "Erro ao consultar no banco";
                }
                finally
                {
                    if (conexao.State == ConnectionState.Open)
                    {
                        conexao.Close();
                    }
                }
                return retornoEmprestimoAlunosLista;
            }
        }
    }

    public class RetornoEmprestimoAluno
    {
        public string NomeAluno { get; set; }
        public string NomeLivro { get; set; }
        public string DataDevolucao { get; set; }
        public string DataFim { get; set; }
    }
}
