using Microsoft.Data.SqlClient;
using System.Data;

namespace TaskManagement.DBLayer
{
    public class DBServices :IDBServices
    {

        private readonly IConfiguration _configuration;

        public DBServices(IConfiguration configuration)
        {
            _configuration= configuration;  
                
        }

        /* ---------- PRIVATE BASICS ---------- */
        private SqlConnection GetConnection()
        {
            var con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            con.Open();
            return con;
        }

        private SqlCommand BuildCmd(SqlConnection con, string sqlOrProc,
                                 CommandType type, IEnumerable<SqlParameter>? prms)
        {
            var cmd = con.CreateCommand();
            cmd.CommandText = sqlOrProc;
            cmd.CommandType = type;

            if (prms is not null)                 // prms ≠ null
                cmd.Parameters.AddRange(prms.ToArray());   // ① convert to array

            return cmd;
        }

        /* ---------- PUBLIC GENERAL METHODS ---------- */

        /// <summary>
        /// SELECT …  – returns DataTable (sync)
        /// </summary>
        public DataTable Query(string sql, IEnumerable<SqlParameter>? prms = null)
        {
            using var con = GetConnection();
            using var cmd = BuildCmd(con, sql, CommandType.Text, prms);
            using var da = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt; // caller can map to DTOs if needed
        }

        /// <summary>
        /// SELECT …  – Returns list of T using a projector Func (sync)
        /// </summary>
        public List<T> Query<T>(string sql, Func<IDataRecord, T> map,
                                IEnumerable<SqlParameter>? prms = null)
        {
            var list = new List<T>();
            using var con = GetConnection();
            using var cmd = BuildCmd(con, sql, CommandType.Text, prms);
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read()) list.Add(map(rdr));
            return list;
        }

        /// <summary>
        /// SELECT … – async DataTable
        /// </summary>
        public async Task<DataTable> QueryAsync(string sql,
                                   IEnumerable<SqlParameter>? prms = null)
        {
            await using var con = GetConnection();
            await using var cmd = BuildCmd(con, sql, CommandType.Text, prms);
            await using var rdr = await cmd.ExecuteReaderAsync();
            var dt = new DataTable();
            dt.Load(rdr);
            return dt;
        }

        /// <summary>
        /// INSERT/UPDATE/DELETE – returns rows affected (sync)
        /// </summary>
        public int Execute(string sql, IEnumerable<SqlParameter>? prms = null)
        {
            using var con = GetConnection();
            using var cmd = BuildCmd(con, sql, CommandType.Text, prms);
            return cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// INSERT/UPDATE/DELETE – async
        /// </summary>
        public async Task<int> ExecuteAsync(string sql,
                            IEnumerable<SqlParameter>? prms = null)
        {
            await using var con = GetConnection();
            await using var cmd = BuildCmd(con, sql, CommandType.Text, prms);
            return await cmd.ExecuteNonQueryAsync();
        }

        /// <summary>
        /// Executes scalar (single cell) query – e.g. SELECT COUNT(*)
        /// </summary>
        public async Task<T?> ScalarAsync<T>(string sql,
                             IEnumerable<SqlParameter>? prms = null)
        {
            await using var con = GetConnection();
            await using var cmd = BuildCmd(con, sql, CommandType.Text, prms);
            object? obj = await cmd.ExecuteScalarAsync();
            return obj == null || obj is DBNull ? default : (T)obj;
        }

        /// <summary>
        /// Stored‑procedure helper (reader) – just change CommandType.
        /// </summary>
        public async Task<DataTable> CallProcAsync(
            string procName, IEnumerable<SqlParameter>? prms = null)
        {
            await using var con = GetConnection();
            await using var cmd = BuildCmd(con, procName, CommandType.StoredProcedure, prms);
            await using var rdr = await cmd.ExecuteReaderAsync();
            var dt = new DataTable();
            dt.Load(rdr);
            return dt;
        }





    }
}
