namespace MetricsCommon.SQL_Settings
{
    public interface ISQLSettings
    {
        public static string ConnectionString { get; }
        public string this[Tables key] { get; }
        public string this[AgentFields key] { get; }
        public string this[ManagerFields key] { get; }

    }
}
