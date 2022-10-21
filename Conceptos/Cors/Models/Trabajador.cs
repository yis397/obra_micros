using MongoDB.Bson.Serialization.Attributes;

namespace Conceptos.Cors.Models
{
    [BsonCollection("trabajador")]
    public class Trabajador:Document
    {
        [BsonElement("nombre")]
        public string Nombre { get; set; } = "";
        [BsonElement("idSql")]
        public string IdSql { get; set; } = "";
        [BsonElement("cargo")]
        public string Cargo { get; set; } = "";
    }
}
