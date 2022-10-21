using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace Conceptos.Cors.Models
{
    [BsonCollection("material")]
    public class Material:Document
    {

        [BsonElement("nombre")]
        public string Nombre { get; set; } = "";
        [BsonElement("unidad")]

        public string Unidad { get; set; } = "";
        [BsonElement("marca")]

        public string Marca { get; set; } = "";
        [BsonElement("precio")]

        public double Precio { get; set; } =0.0;
    }
}
