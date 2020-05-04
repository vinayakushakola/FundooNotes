using System.ComponentModel.DataAnnotations;

namespace CommonLayer.RequestModels
{
    public class ResetPasswordRequest
    {
        [Required]
        public string NewPassword { get; set; }
    }
}
