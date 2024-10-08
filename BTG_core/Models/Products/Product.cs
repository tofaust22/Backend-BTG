using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BTG_core.Models.Products
{
    [BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
         public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("min_amount")]
        public double MinAmount { get; set; }

        [BsonElement("currency")]
        public string Currency {  get; set; }

        [BsonElement("category")]
        public string Category { get; set; }

        [BsonElement("code")]
        public string Code { get; set; }
    }
}
