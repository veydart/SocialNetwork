using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Domain.Models.Request
{
    public class UserRequestModel
    {
        [Required(ErrorMessage = "The name must contain at least 1 character")]
        [RegularExpression(@"^[а-яА-Яa-zA-Z ]+$", ErrorMessage = "Only letters and space are allowed")]
        [MaxLength(64, ErrorMessage = "The name must not exceed 64 characters")]
        public string Name { get; set; }
    }
}