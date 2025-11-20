using System.ComponentModel.DataAnnotations;

namespace ChatBotAI.Application.DTOs.UserDTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage ="Không để trống trường này")]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Không để trống trường này")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Không để trống trường này")]
        public string UserName { get; set; }
    }
}
