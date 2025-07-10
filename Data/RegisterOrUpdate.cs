using System.ComponentModel.DataAnnotations;

namespace LeagueOfLegendsCharachters.Data
{
    public class RegisterOrUpdate
    {
        [Required(ErrorMessage ="UserName is Required Field.")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage ="EmailAddress is Required Field.")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage ="Password is Required Field.")]
        public string Password { get; set; } = string.Empty;
    }
}
