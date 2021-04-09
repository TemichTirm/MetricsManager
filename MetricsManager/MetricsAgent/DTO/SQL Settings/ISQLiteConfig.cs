
using System.Data.SQLite;


namespace MetricsAgent.DTO.SQL_Settings
{
    public interface ISQLiteConfig
    {
        public SQLiteConnection SqlConnection();

        
    }
}
