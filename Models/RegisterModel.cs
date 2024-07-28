using System.ComponentModel.DataAnnotations;

namespace DatingApp.Client.Models
{
    /// <summary>
    /// Represents the registration model for new users.
    /// </summary>
    public class RegisterModel : IValidatableObject
    {
        [Required(ErrorMessage = "First name is required")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Nickname is required")]
        public required string NickName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public required string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        public required string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public required string ConfirmPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!HasUpperCase(Password) || !HasLowerCase(Password) || !HasDigit(Password) || !HasSpecialChar(Password))
            {
                yield return new ValidationResult(
                    "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.",
                    new[] { nameof(Password) });
            }
        }

        private bool HasUpperCase(string str) => str.Any(char.IsUpper);
        private bool HasLowerCase(string str) => str.Any(char.IsLower);
        private bool HasDigit(string str) => str.Any(char.IsDigit);
        private bool HasSpecialChar(string str) => str.Any(c => !char.IsLetterOrDigit(c));
    }
}