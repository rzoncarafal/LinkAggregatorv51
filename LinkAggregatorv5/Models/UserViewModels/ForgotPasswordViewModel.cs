using System.ComponentModel.DataAnnotations;

namespace LinkAggregatorv5.Models.UserViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
