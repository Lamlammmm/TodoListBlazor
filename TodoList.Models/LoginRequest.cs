using System.ComponentModel.DataAnnotations;

namespace TodoList.Models
{
    public class LoginRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string PassWord { get; set; }
    }
}