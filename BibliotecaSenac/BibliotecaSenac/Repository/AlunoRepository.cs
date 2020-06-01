using BibliotecaSenac.Model;
using BibliotecaSenac.Repository.InterfaceRepository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BibliotecaSenac.Repository
{
    public class AlunoRepository : IAlunoRepository
    {
        private readonly IConfiguration configuration;

        public AlunoRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public RetornoTratado<AlunoModel> InserirValidar(AlunoModel aluno, RetornoTratado<AlunoModel> retorno)
        {
            var parametros = PegarParametros(aluno);
            using (IDbConnection conexao = new SqlConnection(configuration.GetValue<string>("SqlConnection")))
            {
                conexao.Open();
                using (IDbTransaction transaction = conexao.BeginTransaction())
                {
                    try
                    {
                        IDbCommand comando = conexao.CreateCommand();
                        comando.Transaction = transaction;

                        comando.CommandText =
                            $@" INSERT INTO 
                                ALUNO  ( NOME  ,  CPF  ,  EMAIL  ,  MATRICULA  ,  TELEFONE  ,  COD_CARTAO)
                                VALUES ('@Nome', '@Cpf', '@Email', '@Matricula', '@Telefone', '@CodCartao')";

                        foreach (var parametro in parametros)
                            comando.CommandText = comando.CommandText.Replace(parametro.Key, parametro.Value);

                        var linhasRetornadas = comando.ExecuteNonQuery();

                        if (linhasRetornadas < 0)
                        {
                            transaction.Rollback();
                            retorno.Erro = true;
                            retorno.MensagemErro = "Erro ao inserir aluno no banco";
                        }
                        else
                        {
                            transaction.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        conexao.Close();

                        retorno.Erro = true;
                        retorno.MensagemErro = "Erro ao inserir Aluno no banco";
                    }
                    finally
                    {
                        conexao.Close();
                    }
                }
            }
            return retorno;
        }

        public RetornoTratado<AlunoModel> AlterarValidar(AlunoModel aluno, RetornoTratado<AlunoModel> retorno)
        {
            var parametros = PegarParametros(aluno);
            using (IDbConnection conexao = new SqlConnection(configuration.GetValue<string>("SqlConnection")))
            {
                conexao.Open();
                using (IDbTransaction transaction = conexao.BeginTransaction())
                {
                    try
                    {
                        IDbCommand comando = conexao.CreateCommand();
                        comando.Transaction = transaction;

                        comando.CommandText =
                            $@" UPDATE
                                ALUNO SET 
                                    NOME       = '@Nome'     ,
                                    CPF        = '@Cpf'      , 
                                    EMAIL      = '@Email'    , 
                                    MATRICULA  = '@Matricula', 
                                    TELEFONE   = '@Telefone' , 
                                    COD_CARTAO = '@CodCartao'
                                WHERE CPF = '@Cpf' OR MATRICULA = '@Matricula'";

                        foreach (var parametro in parametros)
                            comando.CommandText = comando.CommandText.Replace(parametro.Key, parametro.Value);

                        var linhasRetornadas = comando.ExecuteNonQuery();

                        if (linhasRetornadas < 0)
                        {
                            transaction.Rollback();
                            retorno.Erro = true;
                            retorno.MensagemErro = "Erro ao alterar aluno no banco";
                        }
                        else
                        {
                            transaction.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        conexao.Close();

                        retorno.Erro = true;
                        retorno.MensagemErro = "Erro ao alterar Aluno no banco";
                    }
                    finally
                    {
                        conexao.Close();
                    }
                }
            }
            return retorno;
        }

        public RetornoTratado<AlunoModel> DeletarValidar(AlunoModel aluno, RetornoTratado<AlunoModel> retorno)
        {
            var parametros = PegarParametros(aluno);
            using (IDbConnection conexao = new SqlConnection(configuration.GetValue<string>("SqlConnection")))
            {
                conexao.Open();
                using (IDbTransaction transaction = conexao.BeginTransaction())
                {
                    try
                    {
                        IDbCommand comando = conexao.CreateCommand();
                        comando.Transaction = transaction;

                        comando.CommandText =
                            $@" DELETE FROM
                                ALUNO WHERE CPF = '@Cpf' OR MATRICULA = '@Matricula'";

                        foreach (var parametro in parametros)
                            comando.CommandText = comando.CommandText.Replace(parametro.Key, parametro.Value);

                        var linhasRetornadas = comando.ExecuteNonQuery();

                        if (linhasRetornadas < 0)
                        {
                            transaction.Rollback();
                            retorno.Erro = true;
                            retorno.MensagemErro = "Erro ao deletar aluno no banco";
                        }
                        else
                        {
                            transaction.Commit();
                        }
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        conexao.Close();

                        retorno.Erro = true;
                        retorno.MensagemErro = "Erro ao deletar Aluno no banco";
                    }
                    finally
                    {
                        conexao.Close();
                    }
                }
            }
            return retorno;
        }

        public RetornoTratado<AlunoModel> ConsultarValidar(AlunoModel aluno, RetornoTratado<AlunoModel> retorno)
        {
            var parametros = PegarParametros(aluno);
            using (IDbConnection conexao = new SqlConnection(configuration.GetValue<string>("SqlConnection")))
            {
                conexao.Open();
                try
                {
                    IDbCommand comando = conexao.CreateCommand();

                    comando.CommandText =
                        $@" SELECT 
                                ID_ALUNO   ,
                                NOME       ,
                                MATRICULA  ,
                                CPF        ,
                                TELEFONE   ,
                                EMAIL      ,
                                COD_CARTAO
                            FROM ALUNO
                            ";
                    foreach (var parametro in parametros)
                        comando.CommandText = comando.CommandText.Replace(parametro.Key, parametro.Value);

                    var linhasRetornadas = comando.ExecuteReader();

                    while(linhasRetornadas.Read())
                    {
                        retorno.Objeto.IdAluno = (int)linhasRetornadas["ID_ALUNO"];
                        retorno.Objeto.Nome = linhasRetornadas["NOME"].ToString();
                        retorno.Objeto.Matricula = linhasRetornadas["MATRICULA"].ToString();
                        retorno.Objeto.Cpf = linhasRetornadas["CPF"].ToString();
                        retorno.Objeto.Telefone= linhasRetornadas["TELEFONE"].ToString();
                        retorno.Objeto.CodCartao = linhasRetornadas["EMAIL"].ToString();
                        retorno.Objeto.Email = linhasRetornadas["COD_CARTAO"].ToString();
                    }
                    
                }
                catch (Exception e)
                {
                    conexao.Close();

                    retorno.Erro = true;
                    retorno.MensagemErro = "Erro ao consultar no banco";
                }
                finally
                {
                    conexao.Close();
                }
            }
            return retorno;
        }

        private Dictionary<string, string> PegarParametros(AlunoModel aluno)
        {
            Dictionary<string, string> parametros = new Dictionary<string, string>();
            parametros.Add("@Nome", aluno.Nome);
            parametros.Add("@Cpf", aluno.Cpf);
            parametros.Add("@Email", aluno.Email);
            parametros.Add("@Matricula", aluno.Matricula);
            parametros.Add("@Telefone", aluno.Telefone);
            parametros.Add("@CodCartao", aluno.CodCartao);

            return parametros;
        }

    }
}
