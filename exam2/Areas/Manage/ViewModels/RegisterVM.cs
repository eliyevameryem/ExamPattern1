using System.ComponentModel.DataAnnotations;

namespace exam2.Areas.Manage.ViewModels
{
    public class RegisterVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        [MaxLength(32)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]

        public string ConformPassword { get; set;}



    }
}
