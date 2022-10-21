using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Conceptos.Cors.Models
{
    [BsonCollection("concepto") ]
    public class Concepto:Document
    {
        [BsonElement("nombre")]
        public string Nombre { get; set; } = "";
        [BsonElement("descripcion")]
        public string Descripcion { get; set; } = "";
        [BsonElement("cantidad")]
        public double Cantidad { get; set; } = 0.0;
        [BsonElement("diasLaborales")]
        public int DiasLaborales { get; set; } = 0;
        [BsonElement("precio")]
        public double Precio { get; set; } = 0.0;
        [BsonElement("unidad")]
        public string Unidad { get; set; } = "";
        [BsonElement("materiales")]
        public List<SelectMaterial> Materiales { get; set; } = null!;
        public List<SelectTrabajador> Cuadrilla { get; set; } = null!;

        public double CostoMaterial(List<Material> materials)
        {
            double CostoMaterial = 0.0;
            for(var i = 0; i < materials.Count(); i++)
            {
                CostoMaterial += materials[i].Precio;
            }
            return CostoMaterial;
        }

    }
    public class SelectMaterial
    {
        [BsonElement("idmaterial"), BsonRepresentation(BsonType.ObjectId)]
        public string Idmaterial { get; set; } = "";

        [BsonElement("cantidad")]
        public double Cantidad { get; set; } = 0.0;
    }
    public class SelectTrabajador
    {
        [BsonElement("idTrabajador")]
        public string IdTrabajador { get; set; } = "";
        [BsonElement("nombre")]
        public string Nombre { get; set; } = "";
        [BsonElement("cargo")]
        public string Cargo { get; set; } = "";

        [BsonElement("salario")]
        public double Salario { get; set; } = 0.0;
    }
}
