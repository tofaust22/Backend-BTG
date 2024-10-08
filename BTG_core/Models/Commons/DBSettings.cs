namespace BTG_core.Models.Commons
{
    public class DBSettings : IDBSettings
    {
      public string CollectionName { get; set; }
      public string ConnectionString { get; set; }
      public string DatabaseName { get; set; }
    }
}
