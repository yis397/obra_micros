using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Conceptos.Cors.Models
{
    public interface IDocument
    {
        DateTime CreatedDate { get; }
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        string uid { get; set; }
    }

    public class Document : IDocument
    {
        [BsonId, BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string uid { get; set; } = "";
        public DateTime CreatedDate => DateTime.Now;
    }
}
