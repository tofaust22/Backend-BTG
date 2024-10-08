using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace BTG_core.Models.Users
{

    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        public string Id { get; set; }

        [BsonElement("name")]
       public string Name {get; set;}

        [BsonElement("last_name")]
        public string LastName { get; set;}

        [BsonElement("phone")]
        public string Phone { get; set;}

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("date_created")]
        public DateTime DateCreated { get; set;}

        [BsonElement("finance_data")]
        public FinanceData FinanceData { get; set;}
    }

    public class FinanceData
    {
        [BsonElement("amount")]
        public double Amount { get; set;}
    }
}
