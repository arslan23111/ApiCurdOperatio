using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

public interface IDBServices
{
    /* ---------- Query helpers ---------- */

    /// <summary>
    /// Runs a SELECT (or any reader) and returns the rows in a DataTable (sync).
    /// </summary>
    DataTable Query(string sql, IEnumerable<SqlParameter>? prms = null);

    /// <summary>
    /// Runs a SELECT and projects each IDataRecord into T (sync).
    /// </summary>
    List<T> Query<T>(string sql,
                     Func<IDataRecord, T> map,
                     IEnumerable<SqlParameter>? prms = null);

    /// <summary>
    /// Async version that returns a DataTable.
    /// </summary>
    Task<DataTable> QueryAsync(string sql,
                               IEnumerable<SqlParameter>? prms = null);

    /* ---------- Non‑query helpers ---------- */

    /// <summary>
    /// INSERT / UPDATE / DELETE – rows affected (sync).
    /// </summary>
    int Execute(string sql, IEnumerable<SqlParameter>? prms = null);

    /// <summary>
    /// Async INSERT / UPDATE / DELETE.
    /// </summary>
    Task<int> ExecuteAsync(string sql,
                           IEnumerable<SqlParameter>? prms = null);

    /// <summary>
    /// Executes a scalar query (e.g. SELECT COUNT(*)) and casts to T.
    /// </summary>
    Task<T?> ScalarAsync<T>(string sql,
                            IEnumerable<SqlParameter>? prms = null);

    /* ---------- Stored‑procedure helper ---------- */

    /// <summary>
    /// Executes a stored procedure and returns the result as a DataTable.
    /// </summary>
    Task<DataTable> CallProcAsync(string procName,
                                  IEnumerable<SqlParameter>? prms = null);
}
