using System.ComponentModel.DataAnnotations;

namespace CommonLayer.RequestModels
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
