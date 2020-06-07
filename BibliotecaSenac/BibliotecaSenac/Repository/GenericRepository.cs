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
    public abstract class GenericRepository<T> : IGenericRepository<T>
    {
        private readonly IConfiguration configuration;

        public GenericRepository()
        {
        }

        public GenericRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public virtual RetornoTratado<T> Inserir(T objeto, RetornoTratado<T> retorno)
        {
            using (IDbConnection conexao = new SqlConnection(configuration.GetValue<string>("SqlConnection")))
            {
                conexao.Open();
                using (IDbTransaction transaction = conexao.BeginTransaction())
                {
                    try
                    {
                        IDbCommand comando = conexao.CreateCommand();
                        comando.Transaction = transaction;

                        var camposObjetoGenerico = ObterNomePropriedades(objeto);
                        var camposPesquisa = new StringBuilder();
                        foreach (var item in camposObjetoGenerico)
                            if (!(item.ToUpper().Equals("ID" + ObterNomeTabela(objeto).ToUpper())))
                                camposPesquisa = item == camposObjetoGenerico.Last() ? camposPesquisa.Append(item) : camposPesquisa.Append(item + ", ");

                        var parametros = ObterValoresObjeto(objeto);
                        var parametrosPesquisa = new StringBuilder();
                        foreach (var item in parametros)
                            if (!(item.ToUpper().Equals(parametros.First().ToUpper())))
                                parametrosPesquisa = item == parametros.Last() ? parametrosPesquisa.Append(item) : parametrosPesquisa.Append(item + ", ");

                        comando.CommandText =
                            $@" INSERT INTO 
                                {ObterNomeTabela(objeto)}  ({camposPesquisa})
                                VALUES ({parametrosPesquisa})";


                        var linhasRetornadas = comando.ExecuteNonQuery();


                        if (linhasRetornadas < 0)
                        {
                            transaction.Rollback();
                            retorno.Erro = true;
                            retorno.MensagemErro = "Erro ao inserir no banco";
                        }
                        else
                        {
                            transaction.Commit();
                        }
                        conexao.Close();
                    }
                    catch (Exception exception)
                    {
                        transaction.Rollback();

                        retorno.Erro = true;
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

        public virtual RetornoTratado<T> Alterar(T objeto, RetornoTratado<T> retorno, string parametroAlterar)
        {
            using (IDbConnection conexao = new SqlConnection(configuration.GetValue<string>("SqlConnection")))
            {
                conexao.Open();
                using (IDbTransaction transaction = conexao.BeginTransaction())
                {
                    try
                    {
                        IDbCommand comando = conexao.CreateCommand();
                        comando.Transaction = transaction;

                        var camposObjetoGenerico = ObterNomePropriedades(objeto);
                        var camposPesquisa = new StringBuilder();
                        foreach (var item in camposObjetoGenerico)
                            if (!(item.ToUpper().Equals("ID" + ObterNomeTabela(objeto).ToUpper())))
                                camposPesquisa = item == camposObjetoGenerico.Last() ? camposPesquisa.Append(item) : camposPesquisa.Append(item + ", ");

                        var parametros = ObterValoresObjeto(objeto);
                        var parametrosPesquisa = new StringBuilder();
                        foreach (var item in parametros)
                            if (!(item.ToUpper().Equals(parametros.First().ToUpper())))
                                parametrosPesquisa = item == parametros.Last() ? parametrosPesquisa.Append(item) : parametrosPesquisa.Append(item + ", ");

                        var variaveisMudanca = new StringBuilder();
                        var valorClausula = string.Empty;
                        for (int i = 1; i < camposObjetoGenerico.Count(); i++)
                        {
                            if (i == camposObjetoGenerico.Count() - 1)
                                variaveisMudanca.Append(camposObjetoGenerico[i] + "=" + parametros[i]);
                            else
                                variaveisMudanca.Append(camposObjetoGenerico[i] + "=" + parametros[i] + ", ");

                            if (valorClausula == string.Empty)
                                valorClausula = parametroAlterar.ToUpper() == camposObjetoGenerico[i].ToUpper() ? parametros[i] : "";
                        }

                        comando.CommandText =
                            $@" UPDATE {ObterNomeTabela(objeto)} 
                                  SET {variaveisMudanca}
                                WHERE {parametroAlterar} = {valorClausula}
                              ";


                        var linhasRetornadas = comando.ExecuteNonQuery();


                        if (linhasRetornadas < 0)
                        {
                            transaction.Rollback();
                            retorno.Erro = true;
                            retorno.MensagemErro = "Erro ao alterar no banco";
                        }
                        else
                        {
                            transaction.Commit();
                        }
                        conexao.Close();
                    }
                    catch (Exception exception)
                    {
                        transaction.Rollback();

                        retorno.Erro = true;
                        retorno.MensagemErro = "Erro ao alterar no banco";
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

        public virtual RetornoTratado<T> Deletar(T objeto, RetornoTratado<T> retorno, string parametroDeletar)
        {
            using (IDbConnection conexao = new SqlConnection(configuration.GetValue<string>("SqlConnection")))
            {
                conexao.Open();
                using (IDbTransaction transaction = conexao.BeginTransaction())
                {
                    try
                    {
                        IDbCommand comando = conexao.CreateCommand();
                        comando.Transaction = transaction;

                        var camposObjetoGenerico = ObterNomePropriedades(objeto);
                        var parametros = ObterValoresObjeto(objeto);
                        var variaveisMudanca = new StringBuilder();
                        var valorClausula = string.Empty;

                        for (int i = 1; i < camposObjetoGenerico.Count(); i++)
                        {
                            if (i == camposObjetoGenerico.Count() - 1)
                                variaveisMudanca.Append(camposObjetoGenerico[i] + "=" + parametros[i]);
                            else
                                variaveisMudanca.Append(camposObjetoGenerico[i] + "=" + parametros[i] + ", ");
                            valorClausula = parametroDeletar.ToUpper() == camposObjetoGenerico[i].ToUpper() ? parametros[i] : "";
                        }

                        comando.CommandText =
                            $@" DELETE FROM
                                {ObterNomeTabela(objeto)} 
                                WHERE {parametroDeletar} = {valorClausula}";

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
                    catch (Exception exception)
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

        public virtual List<T> Consultar(T objetoGenerico, RetornoTratado<T> retorno)
        {
            var resultSelect = new Dictionary<string, object>();

            using (IDbConnection conexao = new SqlConnection(configuration.GetValue<string>("SqlConnection")))
            {
                var objetoPreenchido = new List<T>();

                try
                {
                    IDbCommand comando = conexao.CreateCommand();

                    var camposObjetoGenerico = ObterNomePropriedades(objetoGenerico);

                    StringBuilder camposPesquisa = new StringBuilder();
                    foreach (var item in camposObjetoGenerico)
                        camposPesquisa = item == camposObjetoGenerico.Last() ? camposPesquisa.Append(item) : camposPesquisa.Append(item + ", ");

                    comando.CommandText =
                        $@" SELECT 
                                {camposPesquisa}
                            FROM {ObterNomeTabela(objetoGenerico)}
                            ";

                    conexao.Open();
                    var linhasRetornadas = comando.ExecuteReader();

                    while (linhasRetornadas.Read())
                    {
                        var stringJson = string.Empty;
                        foreach (var objeto in camposObjetoGenerico)
                        {
                            resultSelect.Add(objeto, linhasRetornadas[objeto.ToUpper().ToString()]);
                        }
                        stringJson = JsonConvert.SerializeObject(resultSelect);

                        objetoPreenchido.Add(JsonConvert.DeserializeObject<T>(stringJson));

                        resultSelect.Clear();
                    }
                    conexao.Close();

                }
                catch (Exception exceptionx)
                {
                    conexao.Close();

                    retorno.Erro = true;
                    retorno.MensagemErro = "Erro ao consultar no banco";
                }
                finally
                {
                    if (conexao.State == ConnectionState.Open)
                    {
                        conexao.Close();
                    }
                }
                return objetoPreenchido;
            }
        }

        public virtual List<T> ConsultarPorParametro(T aluno, RetornoTratado<T> retorno, string parametroConsultar)
        {
            return null;
        }


        private Dictionary<string, object> ObterCampoValoresObjeto(T objeto)
        {
            var tipoEntidade = typeof(T);
            var propriedades = tipoEntidade.GetProperties();
            var sb = new StringBuilder();
            object valorDaPropriedade = null;

            var parametros = new Dictionary<string, object>();

            foreach (var propriedade in propriedades)
            {
                valorDaPropriedade = propriedade.GetValue(objeto, null);

                parametros.Add(propriedade.Name, valorDaPropriedade);
            }

            return parametros;
        }

        private List<string> ObterValoresObjeto(T objeto)
        {
            var camposeValores = ObterCampoValoresObjeto(objeto);
            var valores = new List<string>();

            foreach (var item in camposeValores)
            {
                if (item.Value is int)
                {
                    valores.Add(item.Value.ToString());
                }
                else
                {
                    valores.Add("'" + item.Value.ToString() + "'");
                }
            }


            return valores;
        }

        private List<string> ObterNomePropriedades(T objeto)
        {
            var camposeValores = ObterCampoValoresObjeto(objeto);
            var valores = new List<string>();

            foreach (var item in camposeValores)
                valores.Add(item.Key.ToString());


            return valores;
        }


        private string ObterNomeTabela(T objeto)
        {
            var item = objeto.GetType().Name;

            item = item.Remove(item.Count() - 5, 5); // tira o nome "Model" da classe

            return item;
        }
    }
}
