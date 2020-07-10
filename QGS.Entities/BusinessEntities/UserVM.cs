using System.ComponentModel.DataAnnotations;

namespace QGS.Entities.BusinessEntities
{
    public class UserVM
    {
        [Required]
        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "The Email is required.")]
        [EmailAddress(ErrorMessage = "Email is incorrect")]
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Select a prefix !")]
        public string Prefix { get; set; }
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone Number Required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Please enter Contact Number as 0123456789, 012-345-6789, (012)-345-6789.")]
        public string Contact { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}
