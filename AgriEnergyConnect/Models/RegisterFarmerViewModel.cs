using System.ComponentModel.DataAnnotations;

namespace AgriEnergyConnect.Models
{
    using System.ComponentModel.DataAnnotations;

    namespace AgriEnergyConnect.ViewModels
    {
        public class RegisterFarmerViewModel
        {
            [Required]
            public string Name { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            public string Contact { get; set; }
        }
    }

}
