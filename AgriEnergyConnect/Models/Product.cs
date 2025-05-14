using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgriEnergyConnect.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public DateTime ProductionDate { get; set; }

        public int FarmerID { get; set; }

        [ForeignKey("FarmerID")]
        public Farmer Farmer { get; set; } = null!;
    }
}
