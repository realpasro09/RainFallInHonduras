using System.Data;
using System.IO;

namespace DatabaseDeploymentTool
{
    public static class DbCommandExtensions
    {
        public static void ExecuteSqlFile(this IDbCommand cmd, string filename)
        {
            var tr = new StreamReader(filename);
            var sql = tr.ReadToEnd();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }
    }
}