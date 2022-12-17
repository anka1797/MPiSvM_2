using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace labwork2_web.Data
{
    public class Variant
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string Name { get; set; }

        public double H { get; set; }
        public double t1 { get; set; }
        public double T { get; set; }
        public double w { get; set; }
        public double C { get; set; }
        public double Gm { get; set; }
        public double Cm { get; set; }
        public double d { get; set; }
        public double a { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
    }
}
