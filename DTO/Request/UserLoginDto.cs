using System.ComponentModel.DataAnnotations;

namespace HotelManagementAPI.DTO.Request
{
    public class UserLoginDto
    {
        [Required]
        public bool  IsAdmin { get; set; }

        [Required]
        [MaxLength(12)]
        [RegularExpression(@"^[0-9]*$")]
        public string? Cccd { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
