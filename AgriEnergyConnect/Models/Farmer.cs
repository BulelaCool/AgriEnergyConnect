using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models
{
    public class Farmer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Contact { get; set; }

        public ICollection<Product> Products { get; set; }
        public string Email { get; internal set; }
        public int FarmerID { get; set; }
    }

}
