namespace LiveDataService.Mongo.Models
{
    public class ArchiveDatabaseSettings
    {
        public string? ConnectionString { get; set; } = null;
        public string? DatabaseName { get; set; } = null;
        public string? CollectionName { get; set; } = null;
    }
}
