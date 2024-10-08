using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.Design;

namespace BTG_core.Models.UserProducts
{
    [BsonIgnoreExtraElements]
    public class UserProduct
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("user_id")]
        public string UserId { get; set; }



        [BsonElement("products")]
        public List<ProductItem> Products { get; set; }
    }
    public class ProductItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("active")]
        public bool Active {  get; set; }

        [BsonElement("record")]
        public List<Record> Record {  get; set; }
    }
    public class Record
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("date_created")]
        public DateTime DateCreated { get; set; }

        [BsonElement("current_amount")]
        public double CurrentAmount { get; set; }

        [BsonElement("date_closed")]
        public DateTime DateClosed { get; set; }

        [BsonElement("opening_amount")]
        public double OpeningAmount { get; set; } 
    }
}
