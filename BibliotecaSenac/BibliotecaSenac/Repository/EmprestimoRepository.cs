using BibliotecaSenac.Model;
using BibliotecaSenac.Repository.InterfaceRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BibliotecaSenac.Repository
{
    public class EmprestimoRepository : GenericRepository<EmprestimoModel>, IEmprestimoRepository
    {
        private readonly IConfiguration config;
        private readonly IAlunoRepository alunoRepository;
        private readonly ILivroRepository livroRepository;

        public EmprestimoRepository(IAlunoRepository _alunoRepository, ILivroRepository _livroRepository, IConfiguration _config) : base(_config)
        {
            config = _config;
            alunoRepository = _alunoRepository;
            livroRepository = _livroRepository;
        }

        public override RetornoTratado<EmprestimoModel> Inserir(EmprestimoModel objeto, RetornoTratado<EmprestimoModel> retorno, bool commitTransaction = false, IDbConnection conexao = null, IDbTransaction transaction = null)
        {
            using (conexao = new SqlConnection(config.GetValue<string>("SqlConnection")))
            {
                conexao.Open();
                using (transaction = conexao.BeginTransaction())
                {
                    try
                    {
                        var comando = conexao.CreateCommand();
                        comando.Transaction = transaction;

                        var alunoBusca = alunoRepository.ConsultarComParametroSemPerderConexao(objeto.Aluno, new RetornoTratado<AlunoModel>(), conexao, transaction);
                        if (alunoBusca == null || alunoBusca.Count == 0)
                        {
                            retorno.Erro = true;
                            retorno.MensagemErro = "Aluno não encontrado na base";
                            return retorno;
                        }
                        if (alunoBusca.Count > 1)
                        {
                            retorno.Erro = true;
                            retorno.MensagemErro = "Mais de um aluno encontrado, informar outro parametro de busca";
                            return retorno;
                        }

                        comando.CommandText =
                            $@" INSERT INTO 
                                {ObterNomeTabela(objeto)} 
                                    (DATAINICIO, DATAFIM, DATADEVOLUCAO, IDALUNO)
                                VALUES                    
                                    ('{objeto.DataInicio.ToString("dd/MM/yyy")}', '{objeto.DataFim.ToString("dd/MM/yyy")}', '{objeto.DataDevolucao.ToString("dd/MM/yyy")}', {alunoBusca[0].IdAluno});
                                 ";

                        var linhasRetornadas = comando.ExecuteNonQuery();

                        if (linhasRetornadas < 0)
                        {
                            transaction.Rollback();
                            retorno.Erro = true;
                            retorno.MensagemErro = "Erro ao inserir no banco";
                            return retorno;
                        }

                        comando.CommandText = "SELECT SCOPE_IDENTITY() AS IDEMPRESTIMO";

                        var emprestimoInserido = comando.ExecuteReader();
                        var idEmprestimo = 0;
                        while (emprestimoInserido.Read())
                        {
                            idEmprestimo = (int)emprestimoInserido.GetDecimal(0);
                        }
                        emprestimoInserido.Close();

                        foreach (var livro in objeto.Livros)
                        {
                            var livroBusca = livroRepository.ConsultarComParametroSemPerderConexao(livro, new RetornoTratado<LivroModel>(), conexao, transaction);
                            if (livroBusca == null)
                            {
                                transaction.Rollback();
                                retorno.Erro = true;
                                retorno.MensagemErro = "Livro não encontrado na base";
                                return retorno;
                            }
                            if (livroBusca.Count > 1)
                            {
                                transaction.Rollback();
                                retorno.Erro = true;
                                retorno.MensagemErro = "Mais de um livro encontrado, informar outro parametro de busca";
                                return retorno;
                            }
                            comando.CommandText =
                                $@"
                                INSERT INTO 
                                    EMPRESTIMOLIVRO
                                           (IDLIVRO, IDEMPRESTIMO)
                                VALUES
                                           ({livroBusca[0].IdLivro},{idEmprestimo})
                              ";
                            linhasRetornadas = comando.ExecuteNonQuery();

                            if (linhasRetornadas < 0)
                            {
                                retorno.Erro = true;
                                retorno.MensagemErro += $"Houve um problema ao fazer empréstimo do livro {livroBusca[0].Nome}. ";
                            }
                            else
                            {
                                livroBusca[0].Quantidade -= 1;
                                livroRepository.AlterarSemPerderConexao(livroBusca[0], new RetornoTratado<LivroModel>(), "IdLivro", false, conexao, transaction);

                                transaction.Commit();
                            }
                        }

                        conexao.Close();
                    }
                    catch (Exception exception)
                    {
                        transaction.Rollback();

                        retorno.Erro = true;
                        retorno.ErroLocal = exception.StackTrace;
                        retorno.MensagemErro = "Erro ao inserir no banco";
                    }
                    finally
                    {
                        if (conexao.State == ConnectionState.Open)
                        {
                            conexao.Close();
                        }
                    }
                }
            }
            return retorno;
        }
    }
}
