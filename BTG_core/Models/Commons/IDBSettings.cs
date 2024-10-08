namespace BTG_core.Models.Commons
{
    
        public interface IDBSettings
        {
            string CollectionName { get; set; }
            string ConnectionString { get; set; }
            string DatabaseName { get; set; }
        }
    
}
