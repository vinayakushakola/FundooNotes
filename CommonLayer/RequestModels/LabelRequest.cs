using System.ComponentModel.DataAnnotations;

namespace CommonLayer.RequestModels
{
    public class LabelRequest
    {
        [Required]
        public string LabelName { get; set; }
    }
}
