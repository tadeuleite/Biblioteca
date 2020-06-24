using BibliotecaSenac.Model;
using System.Collections.Generic;
using System.Data;

namespace BibliotecaSenac.Repository.InterfaceRepository
{
    public interface IGenericRepository<T>
    {
        RetornoTratado<T> Inserir(T objeto, RetornoTratado<T> retorno, bool commitTransaction = true, IDbConnection conexao = null, IDbTransaction transaction = null);
        RetornoTratado<T> Alterar(T objeto, RetornoTratado<T> retorno, string parametroAlterar, bool commitTransaction = true, IDbConnection conexao = null, IDbTransaction transaction = null);
        RetornoTratado<T> Deletar(T objeto, RetornoTratado<T> retorno, string parametroDeletar, bool commitTransaction = true, IDbConnection conexao = null, IDbTransaction transaction = null);
        List<T> Consultar(T objeto, RetornoTratado<T> retorno, IDbConnection conexao = null, IDbTransaction transaction = null);
        List<T> ConsultarComParametro(T objeto, RetornoTratado<T> retorno, IDbConnection conexao = null, IDbTransaction transaction = null);
        List<T> ConsultarComParametroSemPerderConexao(T objeto, RetornoTratado<T> retorno, IDbConnection conexao = null, IDbTransaction transaction = null);
        RetornoTratado<T> AlterarSemPerderConexao(T objeto, RetornoTratado<T> retorno, string parametroAlterar, bool commitTransaction = true, IDbConnection conexao = null, IDbTransaction transaction = null);
    }
}
