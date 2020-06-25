using BibliotecaSenac.Model;
using BibliotecaSenac.Repository.InterfaceRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BibliotecaSenac.Repository
{
    public class ReservaRepository : GenericRepository<ReservaModel>, IReservaRepository
    {
        private readonly IConfiguration config;
        private readonly IAlunoRepository alunoRepository;
        private readonly ILivroRepository livroRepository;

        public ReservaRepository(IAlunoRepository _alunoRepository, ILivroRepository _livroRepository, IConfiguration _configuration) : base(_configuration)
        {
            config = _configuration;
            alunoRepository = _alunoRepository;
            livroRepository = _livroRepository;
        }

        public override RetornoTratado<ReservaModel> Inserir(ReservaModel objeto, RetornoTratado<ReservaModel> retorno, bool commitTransaction = false, IDbConnection conexao = null, IDbTransaction transaction = null)
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
                                    (DATAINICIO, DATAFIM, IDALUNO)
                                VALUES                    
                                    ('{objeto.DataInicio.ToString("dd/MM/yyy")}', '{objeto.DataFim.ToString("dd/MM/yyy")}', {alunoBusca[0].IdAluno});
                                 ";

                        var linhasRetornadas = comando.ExecuteNonQuery();

                        if (linhasRetornadas < 0)
                        {
                            transaction.Rollback();
                            retorno.Erro = true;
                            retorno.MensagemErro = "Erro ao inserir no banco";
                            return retorno;
                        }

                        comando.CommandText = "SELECT SCOPE_IDENTITY() AS IDRESERVA";

                        var emprestimoInserido = comando.ExecuteReader();
                        var idReserva = 0;
                        while (emprestimoInserido.Read())
                        {
                            idReserva = (int)emprestimoInserido.GetDecimal(0);
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
                            if (livroBusca[0].Quantidade == 0)
                            {
                                transaction.Rollback();
                                retorno.Erro = true;
                                retorno.MensagemErro = "Livro não disponível para empréstimo, solicitar reserva";
                                return retorno;
                            }

                            comando.CommandText =
                                $@"
                                INSERT INTO 
                                    RESERVALIVRO
                                           (IDLIVRO, IDRESERVA)
                                VALUES
                                           ({livroBusca[0].IdLivro},{idReserva})
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
