using System.ComponentModel.DataAnnotations;

namespace ChatBotAI.Application.DTOs.UserDTO
{
    public class LoginDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
