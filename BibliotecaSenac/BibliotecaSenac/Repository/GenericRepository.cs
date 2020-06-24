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
        protected readonly IConfiguration configuration;

        public GenericRepository()
        {
        }

        public GenericRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public virtual RetornoTratado<T> Inserir(T objeto, RetornoTratado<T> retorno, bool commitTransaction = true, IDbConnection conexao = null, IDbTransaction transaction = null)
        {
            using (conexao = conexao == null ? new SqlConnection(configuration.GetValue<string>("SqlConnection")) : conexao)
            {
                if (conexao.State == ConnectionState.Closed)
                    conexao.Open();
                using (transaction = transaction == null ? conexao.BeginTransaction() : transaction)
                {
                    try
                    {
                        IDbCommand comando = conexao.CreateCommand();
                        comando.Transaction = transaction;

                        var camposObjetoGenerico = ObterNomePropriedades(objeto);
                        var camposPesquisa = new StringBuilder();

                        if (camposObjetoGenerico.Contains("NOMETABELA"))
                            camposObjetoGenerico.Remove("NOMETABELA");

                        foreach (var item in camposObjetoGenerico)
                            if (!item.ToUpper().Equals("ID" + ObterNomeTabela(objeto).ToUpper()))
                                camposPesquisa = item == camposObjetoGenerico.Last() ? camposPesquisa.Append(item) : camposPesquisa.Append(item + ", ");

                        var parametros = ObterValoresObjeto(objeto);
                        var parametrosPesquisa = new StringBuilder();

                        parametros.Remove("'" + ObterNomeTabela(objeto) + "'");
                        foreach (var item in parametros)
                            if (!item.ToUpper().Equals(parametros.First().ToUpper()))
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
                            if (commitTransaction)
                                transaction.Commit();
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

        public virtual RetornoTratado<T> Alterar(T objeto, RetornoTratado<T> retorno, string parametroAlterar, bool commitTransaction = true, IDbConnection conexao = null, IDbTransaction transaction = null)
        {
            using (conexao = conexao == null ? new SqlConnection(configuration.GetValue<string>("SqlConnection")) : conexao)
            {
                if (conexao.State == ConnectionState.Closed)
                    conexao.Open();
                using (transaction = transaction == null ? conexao.BeginTransaction() : transaction)
                {
                    try
                    {
                        IDbCommand comando = conexao.CreateCommand();
                        comando.Transaction = transaction;

                        var camposObjetoGenerico = ObterNomePropriedades(objeto);
                        var camposPesquisa = new StringBuilder();

                        if (camposObjetoGenerico.Contains("NOMETABELA"))
                            camposObjetoGenerico.Remove("NOMETABELA");

                        foreach (var item in camposObjetoGenerico)
                            if (!item.ToUpper().Equals("ID" + ObterNomeTabela(objeto).ToUpper()))
                                camposPesquisa = item == camposObjetoGenerico.Last() ? camposPesquisa.Append(item) : camposPesquisa.Append(item + ", ");

                        var parametros = ObterValoresObjeto(objeto);
                        var parametrosPesquisa = new StringBuilder();
                        foreach (var item in parametros)
                            if (!item.ToUpper().Equals(parametros.First().ToUpper()))
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
                            if (commitTransaction)
                                transaction.Commit();
                        }
                        conexao.Close();
                    }
                    catch (Exception exception)
                    {
                        transaction.Rollback();

                        retorno.Erro = true;
                        retorno.ErroLocal = exception.StackTrace;
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

        public virtual RetornoTratado<T> Deletar(T objeto, RetornoTratado<T> retorno, string parametroDeletar, bool commitTransaction = true, IDbConnection conexao = null, IDbTransaction transaction = null)
        {
            using (conexao = conexao == null ? new SqlConnection(configuration.GetValue<string>("SqlConnection")) : conexao)
            {
                if (conexao.State == ConnectionState.Closed)
                    conexao.Open();
                using (transaction = transaction == null ? conexao.BeginTransaction() : transaction)
                {
                    try
                    {
                        IDbCommand comando = conexao.CreateCommand();
                        comando.Transaction = transaction;

                        var valorClausula = string.Empty;
                        var camposeValores = ObterCampoValoresObjeto(objeto);

                        foreach (var item in camposeValores)
                        {
                            if (parametroDeletar.ToUpper().Equals(item.Key.ToUpper()))
                            {
                                if (item.Value is int)
                                {
                                    valorClausula = item.Value.ToString();
                                }
                                else if (item.Value is string)
                                {

                                    valorClausula = "'" + item.Value.ToString() + "'";
                                }
                            }
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
                            if (commitTransaction)
                                transaction.Commit();
                        }
                    }
                    catch (Exception exception)
                    {
                        transaction.Rollback();
                        conexao.Close();

                        retorno.Erro = true;
                        retorno.ErroLocal = exception.StackTrace;
                        retorno.MensagemErro = "Erro ao deletar Aluno no banco";
                    }
                    finally
                    {
                        if (conexao.State == ConnectionState.Open)
                            conexao.Close();
                    }
                }
            }
            return retorno;
        }

        public virtual List<T> Consultar(T objetoGenerico, RetornoTratado<T> retorno, IDbConnection conexao = null, IDbTransaction transaction = null)
        {
            var resultSelect = new Dictionary<string, object>();

            using (conexao = conexao == null ? new SqlConnection(configuration.GetValue<string>("SqlConnection")) : conexao)
            {
                var objetoPreenchido = new List<T>();

                try
                {
                    IDbCommand comando = conexao.CreateCommand();

                    var camposObjetoGenerico = ObterNomePropriedades(objetoGenerico);

                    StringBuilder camposPesquisa = new StringBuilder();
                    if (camposObjetoGenerico.Contains("NOMETABELA"))
                        camposObjetoGenerico.Remove("NOMETABELA");

                    foreach (var item in camposObjetoGenerico)
                    {
                        camposPesquisa = item == camposObjetoGenerico.Last() ? camposPesquisa.Append(item) : camposPesquisa.Append(item + ", ");
                    }

                    comando.CommandText =
                        $@" SELECT 
                                {camposPesquisa}
                            FROM {ObterNomeTabela(objetoGenerico)}
                            ";

                    if (conexao.State == ConnectionState.Closed)
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
                return objetoPreenchido;
            }
        }

        public virtual List<T> ConsultarComParametro(T objeto, RetornoTratado<T> retorno, IDbConnection conexao = null, IDbTransaction transaction = null)
        {
            var resultSelect = new Dictionary<string, object>();

            using (conexao = conexao == null ? new SqlConnection(configuration.GetValue<string>("SqlConnection")) : conexao)
            {
                var objetoPreenchido = new List<T>();

                try
                {
                    IDbCommand comando = conexao.CreateCommand();
                    if (transaction != null)
                        comando.Transaction = transaction;

                    var propriedades = ObterNomePropriedades(objeto);

                    StringBuilder camposPesquisa = new StringBuilder();
                    if (propriedades.Contains("NOMETABELA"))
                        propriedades.Remove("NOMETABELA");

                    foreach (var item in propriedades)
                    {
                        camposPesquisa = item == propriedades.Last() ? camposPesquisa.Append(item) : camposPesquisa.Append(item + ", ");
                    }

                    comando.CommandText =
                        $@" SELECT 
                                {camposPesquisa}
                            FROM {ObterNomeTabela(objeto)}
                            {ObterWhere(objeto)}
                            ";

                    if (conexao.State == ConnectionState.Closed)
                        conexao.Open();
                    var linhasRetornadas = comando.ExecuteReader();

                    while (linhasRetornadas.Read())
                    {
                        var stringJson = string.Empty;

                        foreach (var propriedade in propriedades)
                        {
                            if (!propriedade.Equals("NOMETABELA"))
                                resultSelect.Add(propriedade, linhasRetornadas[propriedade.ToUpper().ToString()]);
                        }

                        stringJson = JsonConvert.SerializeObject(resultSelect);

                        objetoPreenchido.Add(JsonConvert.DeserializeObject<T>(stringJson));

                        resultSelect.Clear();
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
                return objetoPreenchido;
            }
        }
        public virtual RetornoTratado<T> AlterarSemPerderConexao(T objeto, RetornoTratado<T> retorno, string parametroAlterar, bool commitTransaction = true, IDbConnection conexao = null, IDbTransaction transaction = null)
        {
            try
            {
                IDbCommand comando = conexao.CreateCommand();
                comando.Transaction = transaction;

                var camposObjetoGenerico = ObterNomePropriedades(objeto);
                var camposPesquisa = new StringBuilder();

                if (camposObjetoGenerico.Contains("NOMETABELA"))
                    camposObjetoGenerico.Remove("NOMETABELA");

                foreach (var item in camposObjetoGenerico)
                    if (!item.ToUpper().Equals("ID" + ObterNomeTabela(objeto).ToUpper()))
                        camposPesquisa = item == camposObjetoGenerico.Last() ? camposPesquisa.Append(item) : camposPesquisa.Append(item + ", ");

                var parametros = ObterValoresObjeto(objeto);
                var parametrosPesquisa = new StringBuilder();
                foreach (var item in parametros)
                    if (!item.ToUpper().Equals(parametros.First().ToUpper()))
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
                        valorClausula = parametroAlterar.ToUpper() == camposObjetoGenerico[i - 1].ToUpper() ? parametros[i - 1] : "";
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
                    if (commitTransaction)
                        transaction.Commit();
                }
            }
            catch (Exception exception)
            {
                transaction.Rollback();

                retorno.Erro = true;
                retorno.ErroLocal = exception.StackTrace;
                retorno.MensagemErro = "Erro ao alterar no banco";
            }

            return retorno;
        }
        public virtual List<T> ConsultarComParametroSemPerderConexao(T objeto, RetornoTratado<T> retorno, IDbConnection conexao = null, IDbTransaction transaction = null)
        {
            var resultSelect = new Dictionary<string, object>();

            var objetoPreenchido = new List<T>();

            try
            {
                IDbCommand comando = conexao.CreateCommand();
                comando.Transaction = transaction;

                var propriedades = ObterNomePropriedades(objeto);

                StringBuilder camposPesquisa = new StringBuilder();
                if (propriedades.Contains("NOMETABELA"))
                    propriedades.Remove("NOMETABELA");

                foreach (var item in propriedades)
                {
                    camposPesquisa = item == propriedades.Last() ? camposPesquisa.Append(item) : camposPesquisa.Append(item + ", ");
                }

                comando.CommandText =
                    $@" SELECT 
                                {camposPesquisa}
                            FROM {ObterNomeTabela(objeto)}
                            {ObterWhere(objeto)}
                            ";

                if (conexao.State == ConnectionState.Closed)
                    conexao.Open();
                var linhasRetornadas = comando.ExecuteReader();

                while (linhasRetornadas.Read())
                {
                    var stringJson = string.Empty;

                    foreach (var propriedade in propriedades)
                    {
                        if (!propriedade.Equals("NOMETABELA"))
                            resultSelect.Add(propriedade, linhasRetornadas[propriedade.ToUpper().ToString()]);
                    }

                    stringJson = JsonConvert.SerializeObject(resultSelect);

                    objetoPreenchido.Add(JsonConvert.DeserializeObject<T>(stringJson));

                    resultSelect.Clear();
                }
                linhasRetornadas.Close();
            }
            catch (Exception exception)
            {
                conexao.Close();

                retorno.Erro = true;
                retorno.ErroLocal = exception.StackTrace;
                retorno.MensagemErro = "Erro ao consultar no banco";
            }

            return objetoPreenchido;
        }

        protected Dictionary<string, object> ObterCampoValoresObjeto(T objeto)
        {
            var tipoEntidade = typeof(T);
            var propriedades = tipoEntidade.GetProperties();
            object valorDaPropriedade = null;

            var parametros = new Dictionary<string, object>();

            foreach (var propriedade in propriedades)
            {
                valorDaPropriedade = propriedade.GetValue(objeto, null);

                parametros.Add(propriedade.Name, valorDaPropriedade);
            }

            return parametros;
        }

        protected List<string> ObterValoresObjeto(T objeto)
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

        public List<string> ObterNomePropriedades(T objeto)
        {
            var camposeValores = ObterCampoValoresObjeto(objeto);
            var valores = new List<string>();

            foreach (var item in camposeValores)
                valores.Add(item.Key.ToString());

            return valores;
        }


        protected string ObterNomeTabela(T objeto)
        {
            var camposeValores = ObterCampoValoresObjeto(objeto);
            foreach (var item in camposeValores)
            {
                if (item.Key.ToUpper().Equals("NOMETABELA"))
                    return item.Value.ToString();
            }

            return string.Empty;
        }

        public string ObterWhere(T objeto)
        {
            var where = new StringBuilder();
            where.Append(" WHERE ");

            var camposValores = ObterCampoValoresObjeto(objeto);
            foreach (var item in camposValores)
            {
                if (!item.Key.ToUpper().Equals("NOMETABELA"))
                {
                    if (item.Value is string && !string.IsNullOrEmpty(item.Value.ToString()))
                    {
                        where.Append($" {item.Key} LIKE '%{item.Value}%' OR");
                    }
                    else if (item.Value is int && !item.Value.ToString().Equals("0"))
                    {
                        where.Append($" {item.Key} = {item.Value} OR");
                    }
                }
            }
            where.Remove(where.Length - 3, 3); // tira o ultimo "OR"

            return where.ToString();
        }
    }
}
