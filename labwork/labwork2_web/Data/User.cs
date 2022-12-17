using System.ComponentModel.DataAnnotations;

namespace labwork2_web.Data
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
